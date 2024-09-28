using InterviewTestMid.Models;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Linq;
using System.Runtime.InteropServices;

namespace InterviewTestMid
{

    internal class DoWorkService : InterviewTestMid.IDoWorkService
    {

        private readonly ILogger Log;

        public DoWorkService(ILogger Log)
        {
            this.Log = Log;
        }

        public void DoWork()
        {
            FileStream? FS = null;
            FileStream? FS_New = null;

            try
            {
                Log.WriteLogMessage("InterviewTestMid - Starting");

                String? P = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (P == null)
                {
                    throw new Exception("The Current Directory could not be established");
                }

                FileInfo FI = new FileInfo($"{P}\\Data\\SampleData.json");

                FS = new FileStream(FI.FullName, FileMode.Open, FileAccess.Read);

                JsonSerializerOptions JSO = new JsonSerializerOptions();
                JSO.AllowTrailingCommas = true;

                List<InterviewTestMid.Models.Part>? Ps = JsonSerializer.Deserialize<List<InterviewTestMid.Models.Part>>(FS, JSO);

                if (Ps != null && Ps.Count > 0)
                {
                    Int32 PartIndex = Ps.FindIndex((I) => I.PartDesc.Equals("FOIL", StringComparison.InvariantCultureIgnoreCase));

                    if (PartIndex > -1)
                    {
                        if (Ps[PartIndex].Materials != null && Ps[PartIndex].Materials.Count > 0)
                        {
                            List<String>? Ds = Ps[PartIndex].Materials.Select((S) => S.Material.LookDesc).ToList();

                            if (Ds != null && Ds.Count > 0)
                            {
                                Log.WriteCSVMessage(Ds);
                            }
                            else
                            {
                                throw new Exception("No Material Descriptions were found for the Part FOIL");
                            }

                        }
                        else
                        {
                            throw new Exception("No Materials were found for the Part FOIL");
                        }
                    }
                    else
                    {
                        throw new Exception("The Part FOIL was not found");
                    }

                    PartIndex = new Random().Next(0, (Ps.Count - 1));

                    InterviewTestMid.Models.Part Part_To_Change = Ps[PartIndex].DeepCopy();

                    Part_To_Change.PartWeight.Value = 5;

                    JSO = new JsonSerializerOptions();
                    JSO.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;

                    String X = JsonSerializer.Serialize<InterviewTestMid.Models.Part>(Part_To_Change, JSO);

                    FI = new FileInfo($"{P}\\Data\\{DateTime.Now.ToString("yyyyMMddhhmmssfff")}_PartChanged.json");
                    FS_New = File.Open(FI.FullName, FileMode.Create, FileAccess.Write, FileShare.Read);
                    FS_New.Write(UTF8Encoding.UTF8.GetBytes(X), 0, UTF8Encoding.UTF8.GetByteCount(X));
                    FS_New.Flush();
                }
                else
                {
                    throw new Exception("No Parts were deserialized");
                }

                Log.WriteLogMessage("InterviewTestMid - Finished");

            }
            catch (Exception Ex)
            {
                Log.WriteErrorMessage(Ex);
            }
            finally
            {
                if (FS != null)
                {
                    FS.Close();
                    FS.Dispose();
                }

                if (FS_New != null)
                {
                    FS_New.Close();
                    FS_New.Dispose();
                }

            }
        }

    }

}
