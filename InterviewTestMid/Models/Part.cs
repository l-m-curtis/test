using System.Text.Json.Serialization;

namespace InterviewTestMid.Models
{

    public class Meta
    {

        public Int32 LookId { get; set; }
        public String LookNbr { get; set; }
        public String LookDesc { get; set; }
        public String? LookExtra { get; set; }

        public Meta()
        {
            this.LookNbr = String.Empty;
            this.LookDesc = String.Empty;
        }

    }

    public class PartWeight
    {

        public Int32 UoM { get; set; }
        public Decimal Value { get; set; }

        public PartWeight()
        {
        }

    }

    public class Material
    {

        public Int32 LookId { get; set; }
        public String LookNbr { get; set; }
        public String LookDesc { get; set; }
        public Boolean? MatrIsBarrier { get; set; }
        public Boolean? MatrIsDensifier { get; set; }
        public Boolean? MatrIsOpacifier { get; set; }

        public Material()
        {
            this.LookNbr = String.Empty;
            this.LookDesc = String.Empty;
        }

    }

    public class Materials
    {

        public Material Material { get; set; }
        public Decimal Percentage { get; set; }

        public Materials()
        {
            this.Material = new Material();
        }

    }

    public class Part
    {

        public Int32 PartId { get; set; }
        public String PartNbr { get; set; }
        public String PartDesc { get; set; }
        public Dictionary<String, InterviewTestMid.Models.Meta> Meta { get; set; }
        public InterviewTestMid.Models.PartWeight PartWeight { get; set; }
        public Boolean ConversionsApplied { get; set; }
        public List<InterviewTestMid.Models.Materials> Materials { get; set; }

        public Part()
        {
            this.PartNbr = String.Empty;
            this.PartDesc = String.Empty;
            this.Meta = new Dictionary<String, Meta>();
            this.PartWeight = new PartWeight();
            this.Materials = new List<Materials>();
        }

        public Part DeepCopy()
        {
            Part Part_New = (Part)MemberwiseClone();

            Dictionary<String, Meta> Part_Meta_New = new Dictionary<String, Meta>();

            foreach (KeyValuePair<String, Meta> KVP in Part_New.Meta)
            {
                Part_Meta_New.Add(KVP.Key, new Models.Meta { LookId = KVP.Value.LookId, LookNbr = KVP.Value.LookNbr, LookDesc = KVP.Value.LookDesc, LookExtra = KVP.Value.LookExtra });
            }

            List<Materials> Part_Materials_New = new List<Materials>();

            foreach (Materials Ms in Part_New.Materials)
            {
                Part_Materials_New.Add(new Models.Materials { Material = new Material { LookId = Ms.Material.LookId, LookNbr = Ms.Material.LookNbr, LookDesc = Ms.Material.LookDesc, MatrIsBarrier = Ms.Material.MatrIsBarrier, MatrIsDensifier = Ms.Material.MatrIsDensifier, MatrIsOpacifier = Ms.Material.MatrIsOpacifier },  Percentage = Ms.Percentage });
            }

            Part_New.Meta = Part_Meta_New;
            Part_New.PartWeight = new PartWeight { UoM = Part_New.PartWeight.UoM, Value = Part_New.PartWeight.Value };
            Part_New.Materials = Part_Materials_New;

            return Part_New;
        }

    }

}
