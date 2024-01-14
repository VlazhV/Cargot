using System.Runtime.Serialization;

namespace Autopark.gRPC.Requests;

[DataContract]
public class TimeInterval
{
	[DataMember(Order = 1)]
	DateTime Start { get; set; }
	
	[DataMember(Order = 2)]
	DateTime Finish { get; set; }
}