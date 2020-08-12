using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerUI_CoreExample.Models
{

    public class Class
    {
        [JsonProperty("class")]
        public string class1 { get; set; }
        public double score { get; set; }
        public string type_hierarchy { get; set; }
    }

    public class Classifier
    {
        public string classifier_id { get; set; }
        public string name { get; set; }
        public List<Class> classes { get; set; }
    }

    public class Image
    {
        public List<Classifier> classifiers { get; set; }
        public string image { get; set; }
    }

    public class FaceRecognitionResponse
    {
        public List<Image> images { get; set; }
        public int images_processed { get; set; }
        public int custom_classes { get; set; }
    }


}
