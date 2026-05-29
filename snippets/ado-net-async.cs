// ADO.NET Async Pattern for .NET 4.8
public async Task<IReadOnlyList<Customer>> GetCustomersAsync(CancellationToken ct)
{
    var customers = new List<Customer>();
    using (var conn = new SqlConnection(_connectionString))
    using (var cmd = new SqlCommand("SELECT Id, Name FROM Customers WHERE Active = 1", conn))
    {
        await conn.OpenAsync(ct);
        using (var reader = await cmd.ExecuteReaderAsync(ct))
        {
            while (await reader.ReadAsync(ct))
            {
                customers.Add(new Customer(
                    reader.GetInt32(0),
                    reader.GetString(1)));
            }
        }
    }
    return customers.AsReadOnly();
}
