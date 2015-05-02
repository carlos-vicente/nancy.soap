﻿using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SOAP.Serialization
{
    /// <summary>
    /// Holds the description of a web service. Web Services Description Language (WSDL)
    /// </summary>
    [XmlRoot(
        "definitions",
        Namespace = "http://schemas.xmlsoap.org/wsdl/")]
    public class Definition : IXmlSerializable
    {
        public static readonly string DefaultNamespace = "http://tempuri.org";

        /// <summary>
        /// The target namespace, where the service exists and is unique.
        /// </summary>
        public string TargetNamespace
        {
            get; // TODO: remote set and get obtains the contract namespace
            set;
        }

        /// <summary>
        /// The namespaces and abbreviations for the types and elements used in the definition
        /// </summary>
        public IEnumerable<QNamespace> QualifiedNamespaces { get; set; }

        /// <summary>
        /// The types that can be found in the messages used by this web service.
        /// Defined as xml schemas.
        /// </summary>
        public IEnumerable<Schema> Types { get; set; }

        /// <summary>
        /// The input and output messages used by the web service to transport the data.
        /// </summary>
        public IEnumerable<Message> Messages { get; set; }

        /// <summary>
        /// The interface description, basically the contracts that defines which operations exist
        /// and its input and output messages.
        /// </summary>
        public IEnumerable<PortType> PortTypes { get; set; }

        /// <summary>
        /// The binding between a PortType and the message format and protocol used for message exchanging.
        /// </summary>
        public IEnumerable<Binding> Bindings { get; set; }

        /// <summary>
        /// The service endpoints to call in order to invoke the web service here described.
        /// </summary>
        public IEnumerable<Service> Services { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new System.NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("targetNamespace", this.TargetNamespace);

            AddTypesElement(writer);
        }

        private void AddTypesElement(XmlWriter writer)
        {
            writer.WriteStartElement("types");

            foreach (var schema in Types)
            {
                schema.WriteXml(writer);
            }

            writer.WriteEndElement();

            foreach (var message in Messages)
            {
                message.WriteXml(writer);
            }

            foreach (var portType in PortTypes)
            {
                portType.WriteXml(writer);
            }

            foreach (var binding in Bindings)
            {
                binding.WriteXml(writer);
            }

            foreach (var service in Services)
            {
                service.WriteXml(writer);
            }
        }
    }
}
