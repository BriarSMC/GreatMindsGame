using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using UnityEngine;
using System.Reflection;

/**
 *
 * Copyright © 2025 by Steven M. Coghill
 * This project is licensed under the MIT License.
 * A copy of the MIT License can be found in the 
 * accompanying LICENSE.txt file.
 **/
/** 
 * https://games.coghillclan.net/GreatMinds
 * 
 * https://www.github.com/BriarSMC/GreatMindsGame.git
 *
 * Version: 0.1.0
 * Version History
 * ----------------------------------------------------------------------------
 * 0.1.0    11-Nov-2025 From scratch
 **/

public static class XML
{
  /*
   * Since RPC can't serialize Dictionary<> collections we have to do it ourselves.
   * So we use XML to send collection data over the network.
   * This is a collection of methods to serialize and deserialize our data.
   */

  /*
   * XML Structure
   *
   *  <DictionaryData>
   *    <Data>
   *      <Key>ulong.value</Key>
   *      <Value>string.value</Value>
   *    </Data>
   *  </DictionaryData>
   */

  public static string DataToXML(Dictionary<ulong, string> dict)
  {
    XElement root = new XElement("DictionaryData",
    from kvp in dict
    select new XElement("Data",
      new XElement("Key", kvp.Key),
      new XElement("Value", kvp.Value)));

    return root.ToString();
  }

  public static Dictionary<ulong, string> XMLToData(string xml)
  {
    xml = xml.Replace("\n", "").Replace("\r", "").Trim();
    XDocument doc = XDocument.Parse(xml);
    Dictionary<ulong, string> dict = new Dictionary<ulong, string>();

    foreach (XElement element in doc.Descendants("Data"))
    {
      dict.Add(ulong.Parse(element.Element("Key").Value), element.Element("Value").Value);
    }

    return dict;
  }
}
