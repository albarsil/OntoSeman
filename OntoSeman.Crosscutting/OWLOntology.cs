using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF.Ontology;

namespace OntoSeman.Crosscutting
{
    public class OWLOntology
    {
        public OntologyGraph Ontology { get; set; }
        public ICollection<string> ObjectProperties { get; set; }
        public ICollection<string> Classes { get; set; }
        public ICollection<string> Individuals { get; set; }
    }
}
