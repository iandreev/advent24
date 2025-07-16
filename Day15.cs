namespace Advent24;

internal class Day15
{

    public void Go()
    {
        var input = File.ReadAllLines("input/day15.txt");
        //Console.WriteLine($"result: {Robot(input)}"); // 1415498
        Console.WriteLine($"result: {Robot2(input)}"); // 1415498
    }

    public long Robot(string[] input)
    {
        var maze = ProcessInput(input);
        Show(maze);

        var directions = new Dictionary<char, (int r, int c)>
        {
            { '<', (0, -1) },
            { '>', (0, 1) },
            { '^', (-1, 0) },
            { 'v', (1, 0) },
        };
        foreach (var command in maze.Commands)
        {
            var d = directions[command];
            maze.TryMoveRobot(d.r, d.c);

            //Console.WriteLine(command);
            //Show(maze);
        }


        long result = 0;
        for (int i = 0; i < maze.Map.GetLength(0); i++)
        {
            for (int j = 0; j < maze.Map.GetLength(1); j++)
            {
                if (maze.Map[i, j] == Cell.Barrel)
                {
                    result += i * 100 + j;
                }
            }
        }


        return result;
    }

    public long Robot2(string[] input)
    {
        var maze = ProcessInput2(input);
        Show(maze);

        var directions = new Dictionary<char, (int r, int c)>
        {
            { '<', (0, -1) },
            { '>', (0, 1) },
            { '^', (-1, 0) },
            { 'v', (1, 0) },
        };
        foreach (var command in maze.Commands)
        {
            var d = directions[command];
            maze.TryMoveRobot(d.r, d.c);

            //Console.WriteLine(command);
            //Show(maze);
            //Console.ReadKey();
        }
        Show(maze);


        long result = 0;
        for (int i = 0; i < maze.Map.GetLength(0); i++)
        {
            for (int j = 0; j < maze.Map.GetLength(1); j++)
            {
                if (maze.Map[i, j] == Cell.BarrelL)
                {
                    result += i * 100 + j;
                }
            }
        }

        return result;
    }

    private void Show(Input maze)
    {
        for (int i = 0; i < maze.Map.GetLength(0); i++)
        {
            for (int j = 0; j < maze.Map.GetLength(1); j++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                if (maze.Robot.Row == i && maze.Robot.Col == j)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write('@');
                }
                else
                {
                    Console.Write(maze.At(i, j));
                }
            }
            Console.WriteLine();
        }
    }
    private void Show(Input2 maze)
    {
        for (int i = 0; i < maze.Map.GetLength(0); i++)
        {
            for (int j = 0; j < maze.Map.GetLength(1); j++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                if (maze.Robot.Row == i && maze.Robot.Col == j)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write('@');
                }
                else
                {
                    Console.Write(maze.At(i, j));
                }
            }
            Console.WriteLine();
        }
    }

    private Input ProcessInput(string[] input)
    {
        var mazerows = input.Where(x => x.StartsWith('#')).Count();
        var map = new Cell[mazerows, input[0].Length];
        Robo robot = null!;


        int i = 0;
        for (i = 0; i < mazerows; i++)
        {
            for (int j = 0; j < input[0].Length; j++)
            {
                if (input[i][j] == '.')
                    map[i, j] = Cell.Empty;
                else if (input[i][j] == '#')
                    map[i, j] = Cell.Wall;
                else if (input[i][j] == 'O')
                    map[i, j] = Cell.Barrel;

                if (input[i][j] == '@')
                    robot = new Robo(i, j);
            }
        }
        i++;

        var commands = input[i];
        return new Input(robot, commands, map);
    }

    private Input2 ProcessInput2(string[] input)
    {
        var mazerows = input.Where(x => x.StartsWith('#')).Count();
        var map = new Cell[mazerows, input[0].Length * 2];
        Robo robot = null!;


        int i = 0;
        for (i = 0; i < mazerows; i++)
        {
            int k = 0;
            for (int j = 0; j < input[0].Length; j++)
            {
                if (input[i][j] == '.')
                {
                    map[i, k] = Cell.Empty;
                    map[i, k + 1] = Cell.Empty;
                }
                else if (input[i][j] == '#')
                {
                    map[i, k] = Cell.Wall;
                    map[i, k + 1] = Cell.Wall;
                }
                else if (input[i][j] == 'O')
                {
                    map[i, k] = Cell.BarrelL;
                    map[i, k + 1] = Cell.BarrelR;
                }
                if (input[i][j] == '@')
                {
                    robot = new Robo(i, k);
                    map[i, k] = Cell.Empty;
                    map[i, k + 1] = Cell.Empty;
                }
                k += 2;
            }
        }
        i++;

        var commands = input[i];
        return new Input2(robot, commands, map);
    }

    record Robo(int Row, int Col);
    enum Cell
    {
        Empty,
        Wall,
        Barrel,
        BarrelL,
        BarrelR,
    }

    class Input
    {
        public Robo Robot { get; set; }
        public string Commands { get; set; }
        public Cell[,] Map { get; set; }

        public Input(Robo robot, string commands, Cell[,] map)
        {
            Robot = robot;
            Commands = commands;
            Map = map;
        }

        public void TryMoveRobot(int dr, int dc)
        {
            var r = Robot.Row + dr;
            var c = Robot.Col + dc;

            if (Map[r, c] == Cell.Empty) MoveRobot(dr, dc);
            if (Map[r, c] == Cell.Barrel || Map[r, c] == Cell.BarrelL || Map[r, c] == Cell.BarrelR)
            {
                if (TryMoveB(r, c, dr, dc)) MoveRobot(dr, dc);
            }
        }

        public bool TryMoveB(int r, int c, int dr, int dc)
        {
            var (r2, c2) = OtherB(r, c);
            var (newr2, newc2) = (r2 + dr, c2 + dc);
            var newr = r + dr;
            var newc = c + dc;
            if (Map[newr, newc] == Cell.Wall || Map[newr2, newc2] == Cell.Wall) return false;
            if (Map[newr, newc] == Cell.Barrel || Map[newr, newc] == Cell.BarrelR || Map[newr, newc] == Cell.BarrelL)
            {
                TryMoveB(newr, newc, dr, dc);
            } 
            else if (Map[newr2, newc2] == Cell.Barrel || Map[newr2, newc2] == Cell.BarrelR || Map[newr2, newc2] == Cell.BarrelL)
            {
                TryMoveB(newr2, newc2, dr, dc);
            }

            if (Map[newr, newc] == Cell.Empty && Map[newr2, newc2] == Cell.Empty)
            {
                Map[newr, newc] = Map[r, c];
                Map[newr2, newc2] = Map[r2, c2];
                Map[r, c] = Cell.Empty;
                Map[r2, c2] = Cell.Empty;
                return true;
            }

            return false;
        }
        private void MoveRobot(int dr, int dc)
        {
            Robot = Robot with
            {
                Row = Robot.Row + dr,
                Col = Robot.Col + dc
            };
        }

        private (int r, int c) OtherB(int r, int c)
        {
            if (Map[r, c] == Cell.BarrelL) return (r, c + 1);
            if (Map[r, c] == Cell.BarrelR) return (r, c - 1);
            throw new Exception("Not a barrel");
        }
        public string At(int r, int c)
        {
            switch (Map[r, c])
            {
                case Cell.Empty: return ".";
                case Cell.Wall: return "#";
                case Cell.Barrel: return "O";
                case Cell.BarrelL: return "[";
                case Cell.BarrelR: return "]";
                default: throw new Exception("Unknown cell type");
            }
        }
    };

    class Input2
    {
        public Robo Robot { get; set; }
        public string Commands { get; set; }
        public Cell[,] Map { get; set; }

        public Input2(Robo robot, string commands, Cell[,] map)
        {
            Robot = robot;
            Commands = commands;
            Map = map;
        }

        public void TryMoveRobot(int dr, int dc)
        {
            var r = Robot.Row + dr;
            var c = Robot.Col + dc;

            if (Map[r, c] == Cell.Empty) MoveRobot(dr, dc);
            if (Map[r, c] == Cell.BarrelL || Map[r, c] == Cell.BarrelR)
            {
                if (dr == 0 ? TryMoveBH(r, c, dr, dc) : TryMoveBV(r, c, dr, dc)) MoveRobot(dr, dc);
            }
        }

        public bool TryMoveBH(int r, int c, int dr, int dc)
        {
            var (r2, c2) = OtherB(r, c);
            var (newr, newc) = (r + 2*dr, c + 2*dc);

            if (Map[newr, newc] == Cell.Wall) return false;
            if (Map[newr, newc] == Cell.BarrelR || Map[newr, newc] == Cell.BarrelL)
            {
                TryMoveBH(newr, newc, dr, dc);
            }

            if (Map[newr, newc] == Cell.Empty)
            {
                Map[newr, newc] = Map[r2, c2];
                Map[r2, c2] = Map[r, c];

                Map[r, c] = Cell.Empty;
                return true;
            }

            return false;
        }

        private bool CanMoveBV(int r, int c, int dr)
        {
            if (Map[r, c] == Cell.Wall) return false;
            if (Map[r, c] == Cell.Empty) return true;
            var (r2, c2) = OtherB(r, c);
            if (CanMoveBV(r2 + dr, c2, dr) && CanMoveBV(r + dr, c, dr)) return true;

            return false;
        }
        public bool TryMoveBV(int r, int c, int dr, int dc)
        {
            if (Map[r, c] == Cell.Empty) return true;
            if (Map[r, c] == Cell.Wall) return false;
            var (r2, c2) = OtherB(r, c);
            var (newr2, newc2) = (r2 + dr, c2 + dc);
            var newr = r + dr;
            var newc = c + dc;
            if (Map[newr, newc] == Cell.Wall || Map[newr2, newc2] == Cell.Wall) return false;
            //if (Map[newr, newc] == Cell.BarrelR || Map[newr, newc] == Cell.BarrelL)
            //{
            //    TryMoveBV(newr, newc, dr, dc);
            //}
            //else if (Map[newr2, newc2] == Cell.BarrelR || Map[newr2, newc2] == Cell.BarrelL)
            //{
            //    TryMoveBV(newr2, newc2, dr, dc);
            //}

            if (Map[newr, newc] == Cell.Empty && Map[newr2, newc2] == Cell.Empty)
            {
                Map[newr, newc] = Map[r, c];
                Map[newr2, newc2] = Map[r2, c2];
                Map[r, c] = Cell.Empty;
                Map[r2, c2] = Cell.Empty;
                return true;
            }

            if (CanMoveBV(newr, newc, dr) && CanMoveBV(newr2, newc2, dr))
            {
                TryMoveBV(newr, newc, dr, dc);
                TryMoveBV(newr2, newc2, dr, dc);
            }

            if (Map[newr, newc] == Cell.Empty && Map[newr2, newc2] == Cell.Empty)
            {
                Map[newr, newc] = Map[r, c];
                Map[newr2, newc2] = Map[r2, c2];
                Map[r, c] = Cell.Empty;
                Map[r2, c2] = Cell.Empty;
                return true;
            }

            return false;
        }
        private void MoveRobot(int dr, int dc)
        {
            Robot = Robot with
            {
                Row = Robot.Row + dr,
                Col = Robot.Col + dc
            };
        }

        private (int r, int c) OtherB(int r, int c)
        {
            if (Map[r, c] == Cell.BarrelL) return (r, c + 1);
            if (Map[r, c] == Cell.BarrelR) return (r, c - 1);
            throw new Exception("Not a barrel");
        }
        public string At(int r, int c)
        {
            switch (Map[r, c])
            {
                case Cell.Empty: return ".";
                case Cell.Wall: return "#";
                case Cell.Barrel: return "O";
                case Cell.BarrelL: return "[";
                case Cell.BarrelR: return "]";
                default: throw new Exception("Unknown cell type");
            }
        }
    };
}
