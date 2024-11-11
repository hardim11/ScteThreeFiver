using ScteThreeFiver;


namespace MyProject;

class Program
{
    static void Main(string[] _)
    {
        string[] messages = [
            "/DAlAAAAAA4QAP/wFAUwLMaof+//z0uXAP4AUmXAwlQBAQAAm2zCrQ==",
            "/DAlAAAAAA4QAP/wFAUEqw4Kf+//tqdiAP4AG3dACIsFBQAAcxHpRQ==",
            "/DAvAAAAAAAA///wFAVIAACPf+/+c2nALv4AUsz1AAAAAAAKAAhDVUVJAAABNWLbowo="
        ];

        foreach (string message in messages)
        {
            DoBase64Message(message);
        }
    }

    static void DoBase64Message(string Message)
    {
        Scte35 res = Scte35.DecoderBase64(Message);

        Console.WriteLine();
        Console.WriteLine("Decoding " + Message);
        Console.WriteLine("SCTE Command Type = " + res.SpliceInfoSection.SpliceCommand.GetType().ToString());
        Console.WriteLine("Break duration = " + ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).BreakDuration.Duration.ToString() + " ticks");
        Console.WriteLine("Break duration = " + ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).BreakDuration.DurationSeconds.ToString() + " seconds");
        // company specific details...
        Console.WriteLine("Company Specific - BreakId = " + ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).CompanySpecificInserts.BreakId.ToString());
        Console.WriteLine("Company Specific - BreakTypeId = " + ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).CompanySpecificInserts.TypeId.ToString());
        Console.WriteLine("Company Specific - BreakTypeName = " + ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).CompanySpecificInserts.TypeName);
        Console.WriteLine("Company Specific - SCTE Region ID = " + ((SpliceCommandInsert)(res.SpliceInfoSection.SpliceCommand)).CompanySpecificInserts.ScteRegionId.ToString());
        Console.WriteLine();
    }
}