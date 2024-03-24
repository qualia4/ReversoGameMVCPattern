using Reverso.Model;
using Reverso.View;
using Reverso.Controller;

var game = new ReversoGameWithEvents();
var output = new ConsoleOutput();
output.ListenTo(game);

var input = new ConsoleInput();
input.StartProcessing(game);


