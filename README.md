# agui

- This app should be used through CMD, giving some arguments.

### Install agui

1. Download this file: https://drive.google.com/file/d/1tkP3bRoFhQU2dMimwajeDd4B5uDAYIW8/view?usp=sharing
2. Unzip it.
3. Click setup file and install it.
4. Add to the path system environment variables the folder where the app were installed.
5. Thats all :)

### Manipulate Cursor and Keyboard sequentialy

#### Commands
- `help`: Get help about this program.

- `cpos=[X],[Y]`: Abreviation of cursor pos. Indicates X and Y position in pixels and cursor will go there.

- `cclickl`: Abreviation of cursor click left. Do left click at the current position.

- `cclickr`: Abreviation of cursor click right. Do right click at the current position.

- `cclickupl`: Abreviation of cursor click up left. Up left click at the current position.

- `cclickupr`: Abreviation of cursor click up right. Up right click at the current position.

- `cclickdownl`: Abreviation of cursor click down left. Down left click at the current position.

- `cclickdownr`: Abreviation of cursor click down right. Down right click at the current position.

- `cwheelhor=[value]`: Abreviation of cursor wheel horizontal. Moves the horizontal wheel of the mouse the given value.

- `cwheelver=[value]`: Abreviation of cursor wheel vertical. Moves the vertical wheel of the mouse the given value.

- `delay=[value]`: Indicates a delay in the sequency, in miliseconds.

- `key="[value]"`: Indicates a string to be printed by keyboard.

- `loopstart`: The start a loop.

- `loopend`: Go to the last loopstart sentence until Ctrl+Q key is pressed.

- `cgetpos`: Abreviation of cursor get pos. Get current cursor position and print it.


#### Controls

- Press **Ctrl+N** to go to the next statment immediately.

- Press **Ctrl+Q** to break the loop at the next loopend statement found.

- Press **ESC** to finalize the program immediately.


#### Examples

- `agui help`. Prints extra info about this program.

- `agui cpos=350,100`. Cursor will go to position X=350 and Y=100 in pixels.

- `agui cclickl`. Do left click at the curent position.

- `agui cclickr`. Do right click at the curent position.

- `agui cclickupl`. Up left click at the curent position.

- `agui cclickupr`. Up right click at the curent position.

- `agui cclickdownl`. Down left click at the curent position.

- `agui cclickdownr`. Down right click at the curent position.

- `agui cwheelhor=30`. Moves the horizontal wheel of the mouse 30 units.

- `agui cwheelver=60`. Moves the vertical wheel of the mouse 60 units.

- `agui delay=1000`. Thread sleeps 1000 miliseconds.

- `agui key="hello world!"`. Keyboard press given keys.

- `agui loopstart cclickl loopend`. Do left click until **Ctrl+Q** key is pressed.

- `agui loopstart cclickl loopend`. Do left click until **Ctrl+Q** key is pressed.

- `agui cgetpos`. Prints the current cursor position.


##### Full example

- `agui loopstart cpos=200,150 cclickl delay=500 key="Hello world!" delay=1000 cpos=800,500 loopend`

The previous command causes this sequence:
1. Start a loop.
2. Set cursor position to X=200, Y=150.
3. Do click.
4. Wait 500 miliseconds.
5. Keyboard press Hello world!
6. Wait 1000 miliseconds.
7. Set cursor position to X=800, Y=500
8. Go to 1 until **Ctrl+Q** key is pressed (to break the loop) or **ESC** is pressed (to finalize the program immediately).
