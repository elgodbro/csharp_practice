using Exc1;

MusicalInstrument[] instruments = [new Guitar(), new Piano(), new Drum()];

foreach (var instrument in instruments)
{
    instrument.PlaySound();
}