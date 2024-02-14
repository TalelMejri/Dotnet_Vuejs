import CustomPalette from "./CustomPalette";
import CustomContextPad from "./CustomContextPad";

export default {
  __init__: ["paletteProvider", "contextPadProvider"],
  paletteProvider: ["type", CustomPalette],
  contextPadProvider: ["type", CustomContextPad]
};
