using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VDS.RDF;
using VDS.RDF.Ontology;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Query.Datasets;
using VDS.RDF.Query.Inference;
using VDS.RDF.Writing;

namespace OntoSeman.Crosscutting
{
    public class OWLManager
    {

        public OntologyGraph Ontology { get; set; }

        /** Carrega a ontologia atraves do modulo passado como parametro no construtor
         * */
        public OntologyGraph LoadOntology(string path)
        {
            Ontology = new OntologyGraph();

            FileLoader.Load(Ontology, path);

            return Ontology;
        }

        public void SaveOntology(string path)
        {
            //Assume that the Graph to be saved has already been loaded into a Ontology Propertie
            RdfXmlWriter rdfxmlwriter = new RdfXmlWriter();

            //Save to a File
            rdfxmlwriter.Save(Ontology, path);
        }

        public void Merge(OntologyGraph onto)
        {
            throw new NotImplementedException();
        }

        /**
         * Aplica o Reasoner na ontologia
         * */
        public StaticRdfsReasoner Reasoner()
        {
            RdfsReasoner reasoner = new RdfsReasoner();
            reasoner.Initialise(Ontology); // ontologia base
            reasoner.Apply(Ontology, Ontology);

            return reasoner;
        }

        /**
         * Retorna uma lista com as instancias da classe passada como parametro
         * */
        public ICollection<string> GetIndividualsbyClass(string str)
        {
            ICollection<String> listIndividual = new List<String>();


            // ontologyClass owlClass = ontology.CreateontologyClass(ontology.CreateUriNode("owl:"+str));
            OntologyClass owlClass = null;
            foreach (OntologyClass classe in Ontology.AllClasses)
            {
                if (classe.ToString().Contains(str))
                    owlClass = classe;
            }

            if (owlClass == null)
                return listIndividual;

            foreach (OntologyResource individual in owlClass.Instances)
            {
                listIndividual.Add(individual.ToString());
            }
            return listIndividual;

        }

        public ICollection<String> GetObjectProperties()
        {
            ICollection<String> listObjectProperties = new List<string>();
            foreach (OntologyProperty property in Ontology.OwlObjectProperties)
            {
                listObjectProperties.Add(property.ToString());
            }
            return listObjectProperties;

        }

        /**
  * Retorna uma lista com as instancias de todas as classes da ontologia
  * */
        public ICollection<string> GetIndividuals()
        {
            ICollection<String> listIndividual = new List<String>();


            // ontologyClass owlClass = ontology.CreateontologyClass(ontology.CreateUriNode("owl:"+str));
            foreach (OntologyClass classe in Ontology.AllClasses)
            {
                foreach (OntologyResource individual in classe.Instances)
                {
                    listIndividual.Add(individual.ToString());
                }
            }
            return listIndividual;

        }

        public ICollection<String> GetClasses()
        {
            ICollection<String> listClasses = new List<string>();
            foreach (OntologyClass classe in Ontology.AllClasses)
            {
                listClasses.Add(classe.ToString());
            }
            return listClasses;
        }

        public String GetClass(String individuo)
        {
            foreach (OntologyClass ontologyClasse in Ontology.AllClasses)
            {
                foreach (OntologyResource ontologyIndividual in ontologyClasse.Instances)
                {
                    if (getSignificantWord(ontologyIndividual.ToString(), '#').Equals(individuo))
                        return ontologyClasse.ToString();
                }
            }
            return null;
        }

        /*
        public List<String> GetListaDeObjProDeUmaClass(String classe)
        {
            List<String> propertyList = new List<string>();
            ontologyClass classMatch = null;

            foreach (ontologyClass ontologyClasse in ontology.AllClasses)
            {
                if (getSignificantWord(ontologyClasse.ToString(), '#').Equals(classe))
                {
                    classMatch = ontologyClasse;
                }
            }
            if (classMatch != null)
            {
                foreach (ontologyProperty classProperty in classMatch.IsDomainOf)
                {
                    foreach (ontologyProperty objectProperty in ontology.OwlObjectProperties)
                    {
                        if (objectProperty.Equals(classProperty))
                        {
                            propertyList.Add(classProperty.ToString());
                        }
                    }
                }
                return propertyList;
            }
            else
                return null;
        }  */

        public ICollection<String> GetClassObjectProperties(String ontologyClass)
        {
            ICollection<String> results = new List<string>();
            bool findMatchClass = false;
            foreach (OntologyClass matchClass in Ontology.OwlClasses)
            {
                if (getSignificantWord(matchClass.ToString(), '#').Equals(ontologyClass))
                {
                    ontologyClass = matchClass.ToString();
                    findMatchClass = true;
                    break;
                }
            }
            if (findMatchClass == false)
                return null;

            List<String> queryResults = this.RunQuery("SELECT DISTINCT  ?z WHERE {?x a <" + ontologyClass + ">. ?x ?z ?n.}");
            foreach (String element in queryResults)
                if (element.Contains(Ontology.BaseUri.ToString()) == true)
                {
                    foreach (OntologyProperty objectProperty in Ontology.OwlObjectProperties)
                    {
                        String aux = getSignificantWord(element, '=').Trim();
                        if (objectProperty.ToString().Contains(aux))
                            results.Add(aux);
                    }
                }

            return results;
        }

        public ICollection<String> GetClassDataProperties(String classOntology)
        {
            ICollection<String> propertyList = new List<string>();
            OntologyClass classMatch = null;

            foreach (OntologyClass ontologyClasse in Ontology.AllClasses)
            {
                if (getSignificantWord(ontologyClasse.ToString(), '#').Equals(classOntology))
                {
                    classMatch = ontologyClasse;
                }
            }
            if (classMatch != null)
            {
                foreach (OntologyProperty classProperty in classMatch.IsDomainOf)
                {
                    foreach (OntologyProperty dataProperty in Ontology.OwlDatatypeProperties)
                    {
                        if (dataProperty.Equals(classProperty))
                        {
                            propertyList.Add(classProperty.ToString());
                        }
                    }
                }
                return propertyList;
            }
            else
                return null;
        }

        public ICollection<String> GetIndividualDataPropertie(String individual)
        {
            bool findInstanceFlag = false;
            foreach (OntologyClass classe in Ontology.AllClasses)
            {
                foreach (OntologyResource instance in classe.Instances)
                {
                    if (getSignificantWord(instance.ToString(), '#').Equals(individual))
                    {
                        individual = instance.ToString();
                        findInstanceFlag = true;
                        break;
                    }
                }
            }

            if (findInstanceFlag == false)
                return null;

            List<String> propertyList = new List<string>();
            List<String> queryResults = this.RunQuery("SELECT DISTINCT  ?z WHERE {<" + individual + "> ?z ?n.}");
            foreach (String element in queryResults)
                if (element.Contains(Ontology.BaseUri.ToString()) == true)
                {
                    foreach (OntologyProperty objectProperty in Ontology.OwlDatatypeProperties)
                    {
                        String aux = getSignificantWord(element, '=').Trim();
                        if (objectProperty.ToString().Contains(aux))
                            propertyList.Add(aux);
                    }
                }

            return propertyList;
        }

        public ICollection<String> GetObjectPropertieClass(String objectProperty)
        {
            List<String> propertyList = new List<string>();

            foreach (OntologyProperty property in Ontology.OwlObjectProperties)
            {
                if (getSignificantWord(property.ToString(), '#').Equals(objectProperty))
                {
                    foreach (OntologyClass classe in property.Ranges)
                    {
                        propertyList.Add(classe.ToString());
                    }
                    foreach (OntologyClass classe in property.Domains)
                    {
                        propertyList.Add(classe.ToString());
                    }
                    break;
                }
            }
            return propertyList;
        }

        private String getSignificantWord(String wordLine, char separator)
        {
            return wordLine.Substring(wordLine.LastIndexOf(separator) + 1);
        }

        public ICollection<String> GetObjectPropertieIndividuals(String objectProperty)
        {
            List<String> individualList = new List<string>();

            foreach (OntologyProperty property in Ontology.OwlObjectProperties)
            {
                if (getSignificantWord(property.ToString(), '#').Equals(objectProperty))
                {
                    foreach (OntologyResource individual in property.UsedBy)
                    {
                        individualList.Add(individual.ToString());
                    }
                    break;
                }
            }
            return individualList;
        }

        public ICollection<String> GetCorelatedObjectPropertiesByClass(String classOntology, String objectProperty)
        {
            bool find = false;
            List<String> listClass = new List<string>();

            foreach (OntologyProperty property in Ontology.OwlObjectProperties)
            {
                if (getSignificantWord(property.ToString(), '#').Equals(objectProperty))
                {
                    objectProperty = property.ToString();
                    find = true;
                    break;
                }
            }

            if (find == false)
                return null;

            OntologyClass classMatch = null;
            foreach (OntologyClass classComparer in Ontology.AllClasses)
            {
                if (getSignificantWord(classComparer.ToString(), '#').Equals(Ontology))
                {
                    classMatch = classComparer;
                    break;
                }
            }

            if (classMatch == null)
                return null;

            foreach (OntologyResource individual in classMatch.Instances)
            {
                List<String> individualResults = this.RunQuery("SELECT ?resultado WHERE { <" + individual.ToString() + ">  <" + objectProperty + ">  ?resultado . }");
                if (individualResults != null)
                {
                    foreach (String individualResultString in individualResults)
                    {
                        List<String> classResults = (this.RunQuery("SELECT ?resultado WHERE { <" + getSignificantWord(individualResultString, '=').Trim() + ">  a  ?resultado . }"));
                        if (classResults != null)
                        {
                            foreach (String classResultString in classResults)
                            {
                                if (listClass.Contains(getSignificantWord(classResultString, '=').Trim()) == false && classResultString.Contains(Ontology.BaseUri.ToString()))
                                    listClass.Add(getSignificantWord(classResultString, '=').Trim());
                            }
                        }
                    }
                }
            }
            return listClass;
        }

        public List<String> RunQuery(String queryString)
        {
            queryString = 
            "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> \n " +
            "PREFIX owl: <http://www.w3.org/2002/07/owl#> \n " +
            "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> \n " +
            "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#> \n " + queryString;

            SparqlQueryParser parser = new SparqlQueryParser();
            SparqlQuery query = parser.ParseFromString(queryString);

            Object resultsSet = Ontology.ExecuteQuery(query);
            if (resultsSet is SparqlResultSet)
            {
                SparqlResultSet rset = (SparqlResultSet)resultsSet;
                if (rset.IsEmpty == false)
                {
                    List<String> results = new List<string>();
                    foreach (SparqlResult r in rset)
                    {
                        results.Add(r.ToString());
                    }
                    return results;
                }
                else
                    return null;
            }
            return null;
        }

        public SparqlResultSet RunQueryDataTable(String queryString)
        {
            queryString =
            "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> \n " +
            "PREFIX owl: <http://www.w3.org/2002/07/owl#> \n " +
            "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#> \n " +
            "PREFIX xsd: <http://www.w3.org/2001/XMLSchema#> \n " + queryString;

            SparqlQueryParser parser = new SparqlQueryParser();
            SparqlQuery query = parser.ParseFromString(queryString);

            Object resultsSet = Ontology.ExecuteQuery(query);
            if (resultsSet is SparqlResultSet)
            {
                return (SparqlResultSet)resultsSet;
            }
            else
                return null;
        }
    }
}