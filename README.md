# Gomoku

A Unity3D Demo Project.

![](https://github.com/soasme/Gomoku/raw/master/Demo.gif)

## Idea Explain

- Chess
    - Using fantastic pixel tool http://www.piskelapp.com/ to create two chesses(black/white) within 30 seconds.
    - Define Chess type: BLACK, WHITE.
- Cursor
    - Using fantastic pixel tool http://www.piskelapp.com/ to create cursor image within 1 minute.
    - Move cursor to board center at first.
    - Detect two players' inputs and then move cursor on the board.
    - Make some change on config in Unity InputManger settings.
- Board
    - a N * N two dimensional array, each element is a GameObject.
    - Draw grid at first.
    - Write CheckWinner Algorithm.
    - Make sure after player placing a chess, move cursor to another empty cell.
- Competetion
    - Switch player after one placing a chess.
    - Determine game state(running/end).
    - Show winner if game's ended.
- Toaster
    - Restart a game if game is ended.
