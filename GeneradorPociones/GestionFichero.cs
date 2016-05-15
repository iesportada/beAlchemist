﻿using System.Collections.Generic;
using System;
// -----
using System.IO;
using System.Xml;
using System.Xml.Xsl;


namespace GeneradorPociones
{
    class GestionFichero
    {
        public static string separador = Path.DirectorySeparatorChar.ToString();
        public static string rutaFicheroPocimas = @"..\..\listaParametros.txt";
        public static string[] lineas;

        public static string[] efectoPrim;
        public static string[] efectoSec;

        public static void LeerFichero()
        {
            string todo = File.ReadAllText(rutaFicheroPocimas);
            lineas = todo.Split('-');

            efectoPrim = lineas[0].Split(';');
            efectoSec = lineas[1].Split(';');

        }

        public static string GenerarXML(List<Pocion> lista)
        {
            XmlDocument escritor = new XmlDocument();

            // Declaracion de raíz del documento
            XmlDeclaration declaracion = escritor.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement raiz = escritor.DocumentElement;
            escritor.InsertBefore(declaracion, raiz);

            // Elemento pociones
            XmlElement nodoPociones = escritor.CreateElement("pociones");
            escritor.AppendChild(nodoPociones);


            foreach (Pocion item in lista)
            {
                // Creando elemento pocion 
                XmlNode pocion = escritor.CreateElement("pocion");


                // Generando elementos 
                XmlNode nodoTipo = escritor.CreateElement("tipo");
                nodoTipo.InnerText = item.Tipo;
                pocion.AppendChild(nodoTipo);
                XmlNode nodoPoder = escritor.CreateElement("poder");
                nodoPoder.InnerText = item.Poder;
                pocion.AppendChild(nodoPoder);
                XmlNode nodoEfectoP = escritor.CreateElement("efectoP");
                nodoEfectoP.InnerText = item.EfectoPrim;
                pocion.AppendChild(nodoEfectoP);
                XmlNode nodoEfectoS = escritor.CreateElement("efectoS");
                nodoEfectoS.InnerText = item.EfectoSec;
                pocion.AppendChild(nodoEfectoS);
                XmlNode nodoColor = escritor.CreateElement("color");
                nodoColor.InnerText = item.Color;
                pocion.AppendChild(nodoColor);
                XmlNode nodoDetalle = escritor.CreateElement("detalle");
                nodoDetalle.InnerText = item.Detalle;
                pocion.AppendChild(nodoDetalle);
                XmlNode nodoTextura = escritor.CreateElement("textura");
                nodoTextura.InnerText = item.Textura;
                pocion.AppendChild(nodoTextura);

                // Agregando la pocion al elemento pociones una vez completo
                nodoPociones.AppendChild(pocion);

            }
            // Guardando fichero
            //escritor.Save(ruta);


            return escritor.OuterXml;
        }

        public static void GenerarHTML(string contenido, string ruta)
        {
            string rutaXSL = @"..\..\pociones.xsl";

            XslCompiledTransform transformador = new XslCompiledTransform();
            transformador.Load(rutaXSL);
            //transformador.Transform("omg", ruta);
        }

        public static void GuardarXML(string contenido, string ruta)
        {
            File.WriteAllText(ruta, contenido);
        }

        public static void GenerarRTF(List<Pocion> lista, string ruta)
        {
            string texto = string.Empty;
            string nl = Environment.NewLine;


            foreach (Pocion item in lista)
            {
                texto +=
                    item.Tipo + nl +
                    item.Poder + 
                    item.EfectoPrim +
                    item.EfectoSec + nl +
                    item.Color + nl +
                    item.Detalle + nl +
                    item.Textura
                    + nl + nl;
            }
            File.WriteAllText(ruta, texto, System.Text.Encoding.UTF8);
        }


    }
}
