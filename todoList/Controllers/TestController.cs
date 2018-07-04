using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Linq;

namespace todoList.Controllers
{
    public class TestModel
    {
        public int ID { get; set; }
        public string Commentaire { get; set; }
    }
    public class TestController : ApiController
    {
        //GET : api/test
        public List<TestModel> GetTests()
        {
            List<TestModel> liste = new List<TestModel>();
            /*liste.Add(new TestModel { ID = 42, Commentaire = "la réponse" });
            liste.Add(new TestModel { ID = 39, Commentaire = "température actuelle" });
            liste.Add(new TestModel { ID = 98, Commentaire = "au hasard" });*/

            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));

            return (from x in doc.Descendants("Test")
                    select new TestModel
                    {
                        ID = int.Parse(x.Element("ID").Value),
                        Commentaire = x.Element("Commentaire").Value

                    }).ToList();
        }

        /* var elements = doc.Root.Elements();
         foreach (var item in elements)
         {
             var test = new TestModel
             {
                 ID = int.Parse(item.Element("ID").Value),
                 Commentaire = item.Element("Commentaire").Value
             };
             liste.Add(test);*/


        //GET : api/test/42




        [ResponseType(typeof(TestModel))]

        public IHttpActionResult GetTest(int id)
        {
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            /*
                        TestModel test = null;

                        var elements = doc.Root.Elements();

                        foreach (var item in elements)
                        {
                            if (int.Parse(item.Element("ID").Value == id)

                            {
                                test = new TestModel
                                {
                                    ID = int.Parse(item.Element("ID").Value),
                                    Commentaire = item.Element("Commentaire").Value
                                };
                            }



                            if (test == null)
                            {
                                return NotFound();
                            }
                            return Ok(test);
            */
            //utilisation de la méthode Link pour parcourir les identifiants de la liste de XML
            var test = doc.Descendants("Test").SingleOrDefault(
                x => int.Parse(x.Element("ID").Value) == id);
            if (test == null)
            {
                return NotFound();
            }
            return Ok(new TestModel
            {
                ID = int.Parse(test.Element("ID").Value),
                Commentaire = test.Element("Commentaire").Value
            });
        }

        //POST : api/test
        [ResponseType(typeof(TestModel))]
        public IHttpActionResult PostTest(TestModel test)
        {
            //récupérer le doc XML
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));
            //chercher la valeur max des ID (en faisant appel à une méthode Linq) des éléments "Test"
            var idMax = doc.Descendants("Test").Max(x => int.Parse(x.Element("ID").Value));

            //incrémenter la variable
            idMax++;
            //stockage du nouvel ID dans l'objet testModel
            test.ID = idMax;

            //Création d'une balise XML"Test"
            var element = new XElement("Test");

            //Création des balises enfants "ID" et "Commentaire" avec des valeurs
            element.Add(new XElement("ID", test.ID));
            element.Add(new XElement("Commentaire", test.Commentaire));

            //Ajouter la nouvelle balise dans la balise "Tests"
            doc.Root.Add(element); //doc.Element("Tests").Add(element)

            //Sauvegarder le ficher
            doc.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));


            //Renvoyer un code 201 "Created" avec l'objet mis à jour
            return CreatedAtRoute("DefaultApi", new { id = test.ID }, test);
        }

        //PUT : api/test/id
        [ResponseType(typeof(TestModel))]
        public IHttpActionResult PutTest(int id, TestModel test)
        {
            // tester l'id avec l'id du test
            if (id != test.ID)
            {
                return BadRequest();
            }

            // récupérer le document XML
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));

            //rechercher le Xelement en fonction de l'id
            var elementAModifier = doc.Descendants("Test").SingleOrDefault(
                x => int.Parse(x.Element("ID").Value) == id);

            if (elementAModifier == null)
            {
                return BadRequest();
            }
            //modifier les valeurs avec celles du test
            
            elementAModifier.Element("Commentaire").Value = test.Commentaire;


            // sauvegarder le fichier
            doc.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));

            //retourner le code 204
            return StatusCode(HttpStatusCode.NoContent);

        }
        //DELETE : api/test/id
        [ResponseType(typeof(TestModel))]
        public IHttpActionResult DeleteTest(int id)
        {
            // récupérer le document XML
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));

            //rechercher le XElementen fonction de son Id

            var elementASupprimer = doc.Descendants("Test").SingleOrDefault
                (x => int.Parse(x.Element("ID").Value) == id);
            if(elementASupprimer==null)
            {
                return BadRequest();
            }

            elementASupprimer.Remove();

            // sauvegarder le fichier
            doc.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/donnees.xml"));

            //retourner ok
            return Ok(new TestModel
            {
                ID = id,
                Commentaire = elementASupprimer.Element("Commentaire").Value
            });
        }
    }
}