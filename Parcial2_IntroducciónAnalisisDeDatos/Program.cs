
using System.Globalization;
using System.Numerics;

double capitalInicial = 850000;

List<double> valoresBancoprovincia = new List<double>();
List<double> valoresBancoNacion = new List<double>();
List<double> valoresBancoHipotecario = new List<double>();

cargarPlazosFijos("Banco Provincia", valoresBancoprovincia);
cargarPlazosFijos("Banco Nación", valoresBancoNacion);
cargarPlazosFijos("Banco Hipotecario", valoresBancoHipotecario);

Console.Clear();
// Promedios

double promedioBancoProvincia = calcularPromedio(valoresBancoprovincia);
double promedioBancoNacion = calcularPromedio(valoresBancoNacion);
double promedioBancoHipotecario = calcularPromedio(valoresBancoHipotecario);

Console.WriteLine("Promedio Tasa Anual Banco Provincia " + promedioBancoProvincia + "%");
Console.WriteLine("Promedio Tasa Anual Banco Nación " + promedioBancoNacion  + "%");
Console.WriteLine("Promedio Tasa Anual Banco Hipotecario " + promedioBancoHipotecario + "%");
string mayorPromedio = calcularMayorPromedio(calcularPromedio(valoresBancoprovincia), calcularPromedio(valoresBancoNacion), calcularPromedio(valoresBancoHipotecario));
Console.WriteLine("El banco con mayor Rendimiento en promedio: " + mayorPromedio);
Console.WriteLine("");
Console.WriteLine("Interes por un año completo con la tasa anual promedio");

// Inversión por un año completo con la tasa anual promedio

Console.WriteLine("Total Banco Provincia: " + "$" + inversionUnAño(capitalInicial, promedioBancoProvincia) );
Console.WriteLine("Total Banco Nación: " + "$" + inversionUnAño(capitalInicial, promedioBancoNacion) + "$");
Console.WriteLine("Total Banco Hipotecario: " + "$" + inversionUnAño(capitalInicial, promedioBancoHipotecario)+ "$");

Console.WriteLine("");
Console.WriteLine("Intereses por Trimestres, Calculando el Rendimiento y Reinvirtiendo El Capital Ganado al Finalizar Cada Trimestre.");
// Inversión por trimestres, calculando el rendimiento y reinvirtiendo el capital ganado al finalizar cada trimestre.

Console.WriteLine("Total Banco Provincia: " + "$" + interesPorTrimestres(capitalInicial, promedioBancoProvincia));
Console.WriteLine("Total Banco Nación: " + "$" + interesPorTrimestres(capitalInicial, promedioBancoNacion));
Console.WriteLine("Total Banco Hipotecario: " + "$" + interesPorTrimestres(capitalInicial, promedioBancoHipotecario));

Console.WriteLine("");
Console.WriteLine("Intereses por Meses, Calculando el Rendimiento y Reinvirtiendo El Capital Ganado al Finalizar Cada Mes.");
// inversión por meses, calculando el rendimiento y reinvirtiendo el capital ganado al finalizar cada mes.

Console.WriteLine("Total Banco Provincia: " + "$" + interesPorMes(capitalInicial, promedioBancoProvincia));
Console.WriteLine("Total Banco Nación: " + "$" + interesPorMes(capitalInicial, promedioBancoNacion));
Console.WriteLine("Total Banco Hipotecario: " + "$" + interesPorMes(capitalInicial, promedioBancoHipotecario));


void cargarPlazosFijos(string nombreBanco, List<double> ValoresBanco)
{
    for (int i = 1; i <= 3; i++)
    {
        Console.WriteLine($"{nombreBanco} : Ingrese el valor histórico de los plazos fijos anuales, el {i} año anterior:");

        bool numeroValido = false;
        while (!numeroValido)
        {
            string monto = Console.ReadLine();

            // Reemplaza coma por punto, así el parseo siempre usa el punto como separador decimal
            monto = monto.Replace(",", ".");

            if (double.TryParse(monto, NumberStyles.Any, CultureInfo.InvariantCulture, out double result) && result > 0)
            {
                ValoresBanco.Add(result);
                numeroValido = true;
            }
            else
            {
                Console.WriteLine("El valor ingresado no es correcto. Ingrese un número positivo (puede usar coma o punto).");
            }
        }
    }
}

double calcularPromedio(List<double> valores)
{
    double total = 0;
    foreach (double val in valores)
    {
        total += val;
    }
    total = total / valores.Count;
    return Math.Round(total, 2);
}


string calcularMayorPromedio(double prom1, double prom2, double prom3)
{
    if (prom1 == prom2 && prom2 == prom3)
    {
        return "Los tres bancos tienen el mismo promedio.";
    }

    if (prom1 == prom2 && prom1 > prom3)
    {
        return "Empate entre Banco Provincia y Banco Nación.";
    }

    if (prom1 == prom3 && prom1 > prom2)
    {
        return "Empate entre Banco Provincia y Banco Hipotecario.";
    }

    if (prom2 == prom3 && prom2 > prom1)
    {
        return "Empate entre Banco Nación y Banco Hipotecario.";
    }

    if (prom1 > prom2 && prom1 > prom3)
    {
        return "Banco Provincia";
    }
    else if (prom2 > prom1 && prom2 > prom3)
    {
        return "Banco Nación";
    }
    else if (prom3 > prom1 && prom3 > prom2)
    {
        return "Banco Hipotecario";
    }

    return "";
    
}

double inversionUnAño(double capitalInicial, double promedioAnual) {
    double total;
    total = capitalInicial * (1 + promedioAnual / 100);
    return Math.Round(total, 2);
}

double interesPorTrimestres(double capitalInicial, double promedioAnual)
{
    double total = capitalInicial;
    double promedioTrimestre = promedioAnual / 4;
    for(int i = 0; i<4; i++)
    {
        total *= (1 + promedioTrimestre  / 100);
    }
    
    return Math.Round(total, 2);
}

double interesPorMes(double capitalInicial, double promedioAnual)
{
    double total = capitalInicial;
    double promedioTrimestre = promedioAnual / 12;
    for (int i = 0; i < 12; i++)
    {
        total *= (1 + promedioTrimestre / 100);
    }

    return Math.Round(total, 2);
}