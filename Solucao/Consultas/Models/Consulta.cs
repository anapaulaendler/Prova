//Consulta: Id, PacienteId, DataConsulta, Medico, Diagnostico, Tratamento
public class Consulta
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string PacienteId { get; set; }
    public DateTime DataConsulta { get; set; }
    public required string Medico { get; set; }
    public required string Diagnostico { get; set; }
    public required string Tratamento { get; set; }
}