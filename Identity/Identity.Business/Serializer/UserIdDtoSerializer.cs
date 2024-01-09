using System.Text.Json;
using Confluent.Kafka;
using Identity.Business.DTOs;

namespace Identity.Business.Serializer;

public class UserIdDtoSerializer: ISerializer<UserIdDto>
{
	public byte[] Serialize(UserIdDto data, SerializationContext context)
	{
		using (var stream = new MemoryStream())
		{
			string json = JsonSerializer.Serialize(data);
			var writer = new StreamWriter(stream);

			writer.Write(json);
			writer.Flush();
			stream.Position = 0;
			
			return stream.ToArray();
		}
	}
}