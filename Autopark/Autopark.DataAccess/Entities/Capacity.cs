using System.ComponentModel.DataAnnotations.Schema;

namespace Autopark.DataAccess.Entities;

[NotMapped]
public class Capacity
{
	public int Length { get; set; } //mm
	public int Width { get; set; } //mm
	public int Height { get; set; } //mm
}
