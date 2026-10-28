using Tarea;

var cuenta1 = new CuentaBancaria("Alexander", "001", 100000m);
var cuenta2 = new CuentaBancaria("Camila", "002"); 

bool deposito = cuenta1.Depositar(50000m);
Console.WriteLine($"Depósito de 50000: {(deposito ? "OK" : "Rechazado")} | Saldo: {cuenta1.Saldo:C}");

bool retiro = cuenta1.Retirar(500000m);
Console.WriteLine($"Retiro de 500000: {(retiro ? "OK" : "Rechazado")} | Saldo: {cuenta1.Saldo:C}");

bool transferencia = cuenta1.Transferir(cuenta2, 30000m);
Console.WriteLine($"Transferencia de 30000: {(transferencia ? "OK" : "Rechazada")}");
Console.WriteLine($"{cuenta1.Titular} ({cuenta1.Numero}): {cuenta1.Saldo:C}");
Console.WriteLine($"{cuenta2.Titular} ({cuenta2.Numero}): {cuenta2.Saldo:C}");

Console.ReadKey();