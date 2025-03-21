using Exc5;

MusicalInstrument[] instruments = [
    new Guitar(),
    new Piano()
];

foreach (var instrument in instruments)
{
    instrument.Tune();
    instrument.PlaySound();
    Console.WriteLine();
}