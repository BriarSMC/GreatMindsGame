using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine.Rendering;

namespace CoghillClan.PanelManager
{

    /**
     *
     * Copyright © 2025 by Steven M. Coghill
     * This project is licensed under the MIT License.
     * A copy of the MIT License can be found in the 
     * accompanying LICENSE.txt file.
     **/

    /** 
     * https://games.coghillclan.net/
     * 
     * https://www.github.com/BriarSMC/
     *
     * Version: 0.1.2
     * Version History
     * ----------------------------------------------------------------------------
     * 0.1.0    13-Oct-2025 From scratch
     * 0.1.1    30-Oct-2025 Add new Panel class capabilities
     * 0.1.2    01-Nov-2025 Fix calling OnPanelLoaded() when FindPanels() is called
     *                      No longer call Panel.SetPanelManager()
     **/
    public class PanelManager : MonoBehaviour
    {

        [SerializeReference] Panel initialPanel;

        public const string Version = "0.1.2";
        public List<Panel> managedPanels;
        public List<Panel> panelStack = new List<Panel>();

        /**
         * Public Methods
         **/

        public void ManagerEnable(bool enable)
        {
            /**
             * Enable is the same as initializing the panel manager. 
             * So we clear the managed panels and panel stack.
             * Load the child panels.
             * Disable we just turn all of the panels off.
             **/
            if (enable)
            {
                managedPanels.Clear();
                panelStack.Clear();
                LoadPanels();
            }
            else
                TurnAllPanelsOff();
        }

        public void LoadPanels()
        {
            // If you have multiple scenes that use a persistent game manager that controls the PanelManager,
            // then you have to call LoadPanels() to get the new scene's panels.

            /**
            * Die if no managed panels exist.
            * If initialPanel isn't set, then just use the first one in the managed panels.
            * Disable all panels.
            * Push the initial panel onto the stack.
            **/
            FindPanels();
            // TODO: Need to throw an exception if managedPanels contains duplicate names.
            TurnAllPanelsOff();
            if (managedPanels.Count == 0) throw new ApplicationException("PanalManager: No panels found.");
            if (initialPanel == null) { initialPanel = managedPanels[0]; }
            Push(initialPanel); // Put it on the stack
        }

        //TODO Consider making the next 3 methods "private"
        public void TurnAllPanelsOff() { foreach (Panel panel in managedPanels) { panel.gameObject.SetActive(false); } }
        public void TurnPanelOff(Panel panel)
        {
            panel.gameObject.SetActive(false);
            panel.OnPanelDisabled();
        }
        public void TurnPanelOn(Panel panel)
        {
            panel.gameObject.SetActive(true);
            panel.OnPanelEnabled();
        }

        public int AddManagedPanel(Panel panel)
        {
            /**
             * Throw an exception if the panel is not in our list.
             * Add the panel to our managedPanels list.
             * Return the index in the managedPanels list of the new panel.
             *
             * Note: This is left over from when we had each Panel object call PanelManager to 
             * add itself to the managedPanels list. We refactored so that the PanelManager now
             * finds all of its child Panel objects and adds them to the list. We are keeping it in
             * because if we ever allow for dynamically created panels, then this will the be way 
             * to add them to our list.
             **/

            if (IsPanelManaged(panel)) { throw new ApplicationException($"Panel already exists. Not added. Name: {panel.PanelName}"); }
            managedPanels.Add(panel);
            panel.OnPanelLoaded();
            return managedPanels.Count - 1;
        }

        public int GetManagedPanelCount() { return managedPanels.Count; } //TODO Possibly remove method
        public int GetPanelStackCount() { return panelStack.Count; }

        public int Push(string name) { return Push(FindManagedPanel(name)); }
        public int Push(Panel panel)
        {
            /**
             * If the panel is in the managed list, then we can process it
             * We need to make the current panel (last one in the stake) disabled. (If it exists.)
             * Add the panel to the stack.
             * Enable it.
             * Return the stack index of the entry.
             *
             * Otherwise, bitch, whine and moan about it.
             **/
            if (FindManagedPanel(panel))
            {
                panelStack.Add(panel);
                if (panelStack.Count >= 2) TurnPanelOff(panelStack[panelStack.Count - 2]);
                TurnPanelOn(panel);
                panel.OnPanelEnabled();
                return panelStack.Count - 1;
            }
            else
            {
                throw new ApplicationException("Cannot Push Panel. Panel not found in managed list.");
            }
        }

        public int Pop() { return Pop(1); }
        public int Pop(int count)
        {
            /** 
             * If there are not enough panels on the stack, then we will just remove everything after the first panel.
             * Otherwise, remove count entries.
             **/

            if (panelStack.Count <= 0) throw new ApplicationException("Panel stack is empty. Nothing to pop.");
            if (panelStack.Count == 1) return 0; // Can't pop off only entry


            if (count == 1) // <= panelStack.Count)
            {
                TurnPanelOff(panelStack[panelStack.Count - 1]);
                panelStack.RemoveRange(panelStack.Count - 1, 1);
            }
            else
            {
                for (int i = panelStack.Count - count; i < panelStack.Count; i++)
                {
                    TurnPanelOff(panelStack[i]);
                }
                panelStack.RemoveRange(panelStack.Count - count, count);
            }
            // panelStack.RemoveRange(panelStack.Count - count, count);

            TurnAllPanelsOff();
            TurnPanelOn(panelStack[panelStack.Count - 1]);
            return panelStack.Count;
        }

        public int PopTo(string name)
        {
            int ndx = panelStack.FindIndex(x => x.PanelName == name);
            return PopTo(ndx);
        }

        public int PopTo(Panel panel)
        {
            int ndx = panelStack.IndexOf(panel);
            return PopTo(ndx);
        }

        public int PopTo(int ndx)
        {
            int i = panelStack.Count - 1;
            while (i > ndx)
            {
                TurnPanelOff(panelStack[i--]);
            }
            DeleteStackFromIndex(ndx); // Will also turn last stack element on
            return ndx;
        }

        public void Swap()
        {
            if (panelStack.Count <= 1) return;
            Panel hold = panelStack[panelStack.Count - 1];
            panelStack[panelStack.Count - 1] = panelStack[panelStack.Count - 2];
            panelStack[panelStack.Count - 2] = hold;
            TurnPanelOff(hold);
            TurnPanelOn(panelStack[panelStack.Count - 1]);

        }

        /** 
         * Private Methods
         **/

        private bool IsPanelManaged(Panel panel) { return managedPanels.SingleOrDefault(p => p.PanelObject == panel) != null ? true : false; }
        private bool IsPanelManaged(GameObject panel) { return managedPanels.SingleOrDefault(p => p.PanelObject == panel) != null ? true : false; }
        private bool IsPanelManaged(string name) { return managedPanels.SingleOrDefault(p => p.PanelName == name) != null ? true : false; }

        private Panel FindManagedPanel(string name) { return managedPanels.SingleOrDefault(p => p.PanelName == name); }
        private Panel FindManagedPanel(Panel panel)
        {
            return managedPanels.SingleOrDefault(p => p.PanelObject == panel.gameObject);
        }
        private Panel FindManagedPanel(int i) { return managedPanels[Math.Clamp(i, 0, managedPanels.Count)]; }

        //TODO Possibly delete this method
        private Panel GetStackPanel(int ndx) { return panelStack.Single(i => i.PanelIndex == ndx); }
        private Panel GetStackPanel(string name) { return panelStack.Single(n => n.PanelName == name); }
        private Panel GetStackPanel(GameObject obj) { return panelStack.Single(o => o.PanelObject); }


        private void FindPanels()
        {
            /**
             * Find all the children panels and load them into managedPanels
             **/
            managedPanels = new List<Panel>(GetComponentsInChildren<Panel>());
            foreach (Panel panel in managedPanels) { SetFoundPane(panel); }
        }

        // Everything we need to do when we find panels and add them to the 
        // managed panel list.
        private void SetFoundPane(Panel panel)
        {
            panel.OnPanelLoaded();
        }

        private void DeleteStackFromIndex(int ndx)
        {
            if (ndx == -1) throw new ApplicationException("Could not pop to requested panel. Panel not found.");
            TurnAllPanelsOff();
            panelStack.RemoveRange(ndx + 1, panelStack.Count - ndx - 1);
            TurnPanelOn(panelStack[ndx]);
        }
    } // End of Class PanelManager
} // End of Namespace CoghillClan.PanelManager Coghill
