using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;

namespace SDCourseProject
{
    public class CourseProject : Control
    {
        //Объекты

        Random random = new Random();

        //Внутренние поля с публичными свойствами

        private int _xPlayField;
        private int _yPlayField;
        private int _widthPlayField;
        private int _heightPlayField;
        private int _xPlayBall;
        private int _yPlayBall;
        private int _xDefeatLine;
        private int _yDefeatLine;
        private int _colPlayBall;
        private int _score;
        private int _quantityColors;

        private bool _isAskToGameComplete;

        private Color _colorPlayFieldFrame;
        private Color _colorDefeatLine;

        private Color _firstBallColor;
        private Color _secondBallColor;
        private Color _thirdBallColor;
        private Color _fourthBallColor;
        private Color _fifthBallColor;

        //Внутренние поля без публичных свойств

        private int ballStartY;//Относительно игрового поля

        public bool isStartGame = false;
        public bool isBallMoving = false;

        //Глобальные объекты

        Brush brushPlayBall = new SolidBrush(Color.Empty);

        //Константы

        const int _BoxWidth = 10;
        const int _BoxHeight = 25;
        const int _StartQuantityBalls = 15;
        const int _LimitRowsBalls = 20;
        const int _LimitQuantityColors = 5;
        const int _BallSize = 20;
        const int _BallStep = 2;
        const int _ScorePerBall = 100;

        //Массивы

        Color[,] Balls_Array = new Color[_BoxHeight, _BoxWidth];
        Color[] StoreColors = new Color[_LimitQuantityColors];
        int[,] Island_Array = new int[_LimitRowsBalls, _BoxWidth];
        List<Ball> DestroyBallsList = new List<Ball>(_BoxWidth * _LimitRowsBalls);


        //Объекты - события

        private event EventHandler _onStartGame;
        private event EventHandler _onScoreChanged;
        private event EventHandler _onEndGame;

        //Конструктор класса

        public CourseProject()
        {
            FirstBallColor = Color.Maroon;
            SecondBallColor = Color.Yellow;
            ThirdBallColor = Color.Green;
            FourthBallColor = Color.Blue;
            FifthBallColor = Color.Pink;
            IsAskToGameComplete = false;
        }

        //Публичные свойства

        public int XPlayField
        {
            get => _xPlayField;
            set
            {
                value = (Size.Width / 2) - WidthPlayField / 2;

                if (value > Size.Width - WidthPlayField) { value = Size.Width - WidthPlayField; }
                if (value < 0) { value = 0; }
                if (_xPlayField != value) { _xPlayField = value; Invalidate(); }
            }
        }

        public int YPlayFiled
        {
            get => _yPlayField;
            set
            {
                value = (Height / 2) - HeightPlayField / 2;

                if (value > Size.Height - HeightPlayField) { value = Size.Height - HeightPlayField; }
                if (value < 0) { value = 0; }
                if (_yPlayField != value) { _yPlayField = value; Invalidate(); }

            }
        }

        public int WidthPlayField
        {
            get => _widthPlayField;
            set
            {
                value = _BallSize * _BoxWidth;

                if (_widthPlayField != value)
                {
                    _widthPlayField = value;

                    Invalidate();
                }
            }
        }

        public int HeightPlayField
        {
            get => _heightPlayField;
            set
            {
                value = +_BallSize * _BoxHeight;

                if (_heightPlayField != value)
                {
                    _heightPlayField = value; Invalidate();
                }
            }
        }

        public int XPlayBall
        {
            get => _xPlayBall;
            set
            {
                value = XPlayField + _BallSize * ColPlayBall;
                if (value > XPlayField + WidthPlayField) { value = XPlayField + WidthPlayField; }
                if (value < XPlayField) { value = XPlayField; }
                if (_xPlayBall != value) { _xPlayBall = value; Invalidate(); }
            }
        }

        public int YPlayBall
        {
            get => _yPlayBall;
            set
            {
                if (value > YPlayFiled + HeightPlayField - _BallSize) { value = YPlayFiled + HeightPlayField - _BallSize; }
                if (value < 0) { value = 0; }
                if (_yPlayBall != value) { _yPlayBall = value; Invalidate(); }
            }
        }

        public int XDefeatLine
        {
            get => _xDefeatLine;
            set
            {
                value = XPlayField;
                if (_xDefeatLine != value)
                {
                    _xDefeatLine = value; Invalidate();
                }
            }
        }

        public int YDefeatLine
        {
            get => _yDefeatLine;
            set
            {
                value = YPlayFiled + _BallSize * _LimitRowsBalls;
                if (_yDefeatLine != value)
                {
                    _yDefeatLine = value; Invalidate();
                }
            }
        }

        public int ColPlayBall
        {
            get => _colPlayBall;
            set
            {
                if (value > _BoxWidth - 1) { value = _BoxWidth - 1; }
                if (value < 0) { value = 0; }

                if (_colPlayBall != value) { _colPlayBall = value; Invalidate(); }
            }
        }

        public int Score
        {
            get => _score;
            set { if (_score != value) { _score = value; DoScoreChanged(); } }
        }

        public int QuantityColors
        {
            get => _quantityColors;
            set
            {
                if (value > _LimitQuantityColors) { value = _LimitQuantityColors; }
                if (value < 2) { value = 2; }
                if (_quantityColors != value) { _quantityColors = value; Invalidate(); }
            }
        }

        public bool IsAskToGameComplete
        {
            get => _isAskToGameComplete;
            set { if (_isAskToGameComplete != value) { _isAskToGameComplete = value; } }
        }

        public Color ColorPlayFieldFrame
        {
            get => _colorPlayFieldFrame;

            set
            {
                if (_colorPlayFieldFrame != value) { _colorPlayFieldFrame = value; Invalidate(); }
            }
        }

        public Color ColorDefeatLine
        {
            get => _colorDefeatLine;

            set
            {
                if (_colorDefeatLine != value) { _colorDefeatLine = value; Invalidate(); }
            }
        }

        public Color FirstBallColor
        {
            get => _firstBallColor;
            set { if (_firstBallColor == Color.Empty) { value = Color.Transparent; } if (_firstBallColor != value) { _firstBallColor = value; Invalidate(); } }
        }
        public Color SecondBallColor
        {
            get => _secondBallColor;
            set
            { if (_secondBallColor == Color.Empty) { value = Color.Transparent; } if (_secondBallColor != value) { _secondBallColor = value; Invalidate(); } }
        }
        public Color ThirdBallColor
        {
            get => _thirdBallColor;
            set { if (_thirdBallColor == Color.Empty) { value = Color.Transparent; } if (_thirdBallColor != value) { _thirdBallColor = value; Invalidate(); } }
        }
        public Color FourthBallColor
        {
            get => _fourthBallColor;
            set { if (_fourthBallColor == Color.Empty) { value = Color.Transparent; } if (_fourthBallColor != value) { _fourthBallColor = value; Invalidate(); } }
        }
        public Color FifthBallColor
        {
            get => _fifthBallColor;
            set { if (_fifthBallColor == Color.Empty) { value = Color.Transparent; } if (_fifthBallColor != value) { _fifthBallColor = value; Invalidate(); } }
        }

        //Методы

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen penPlayField = new Pen(ColorPlayFieldFrame);
            Pen penDefeatLine = new Pen(ColorPlayFieldFrame);
            //Игровое поле
            e.Graphics.DrawRectangle(penPlayField, XPlayField, YPlayFiled, WidthPlayField, HeightPlayField);
            //Граница поражения
            e.Graphics.DrawLine(penDefeatLine, XDefeatLine, YDefeatLine, XDefeatLine + WidthPlayField, YDefeatLine);
            if (isStartGame)
            {



                //Отрисовка существующих шариков
                Brush ball;
                for (int i = 0; i < _BoxHeight; i++)
                {
                    for (int j = 0; j < _BoxWidth; j++)
                    {
                        ball = new SolidBrush(GetColor(j, i));
                        e.Graphics.FillEllipse(ball, _BallSize * j + XPlayField, YPlayFiled + _BallSize * i, _BallSize, _BallSize);
                    }
                }

                //Управляемый шарик
                e.Graphics.FillEllipse(brushPlayBall, XPlayBall, YPlayBall, _BallSize, _BallSize);


                //Удаление кисти
                penPlayField.Dispose();
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            }
        }

        public void StartGame()
        {
            SetStartGameValues();

            isStartGame = true;

            MixBalls();

            CreatePlayBall();

            DoStartGame();
        }
        public void EndGame()
        {
            isStartGame = false;
            Invalidate();
            DoEndGame();
        }

        public void SetStartGameValues()
        {
            WidthPlayField = _BallSize * _BoxWidth;
            HeightPlayField = _BallSize * _BoxHeight;

            XPlayField = (Width / 2) - WidthPlayField / 2;
            YPlayFiled = (Height / 2) - HeightPlayField / 2;

            ColPlayBall = (_BoxWidth / 2) - 1;

            XPlayBall = XPlayField + _BallSize * _colPlayBall;
            YPlayBall = YPlayFiled + HeightPlayField - _BallSize;

            ballStartY = YPlayBall;

            Score=0;
        }

        private void MixBalls()
        {
            for (int i = 0; i < _StartQuantityBalls; i++)
            {
                for (int j = 0; j < _BoxWidth; j++)
                {
                    SetColor(j, i, GetRandomColor(QuantityColors));
                }
            }
            Invalidate();
        }

        private void CreatePlayBall()
        {
            brushPlayBall = new SolidBrush(GetRandomColor(QuantityColors));
            YPlayBall = ballStartY;
        }

        public void LaunchPlayBall()
        {
            isBallMoving = true;

            int row;

            do
            {
                row = (YPlayBall - YPlayFiled) / _BallSize;
                YPlayBall -= _BallStep;
                //Если ось Y шарика кратна одной из строк
                if (CheckOnMultipleRow())
                {
                    if (CheckOnNeedStopPlayBall(row))
                    {
                        StopPlayBall(row);
                    }

                    Thread.Sleep(10);
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            while (isBallMoving);
        }

        //Проверка того, кратна ли текущая высока шарика строке массива
        private bool CheckOnMultipleRow()
        {
            if ((YPlayBall - YPlayFiled) % _BallSize == 0)
            {
                return true;
            }
            return false;
        }

        private bool CheckOnNeedStopPlayBall(int row)
        {
            if ((row > 0 && GetColor(ColPlayBall, row - 1) != Color.Empty) || row == 0)
            {
                return true;
            }

            return false;
        }

        private void StopPlayBall(int row)
        {
            Color playBallColor = ((SolidBrush)(brushPlayBall)).Color;

            isBallMoving = false;

            SetColor(ColPlayBall, row, playBallColor);

            if (CheckOnDefeat())
            {
                Defeat();
            }
            else
            {
                HandlerDestroyBalls(ColPlayBall, row, playBallColor);
            }

            CreatePlayBall();
        }

        private bool CheckOnDefeat()
        {

            if (YPlayBall >= YDefeatLine)
            {
                return true;
            }

            return false;

        }

        private void Defeat()
        {
            DialogResult dialogResult = MessageBox.Show("Начать заного?", "Поражение", MessageBoxButtons.YesNo);
            switch (dialogResult)
            {
                case DialogResult.Yes:
                    {
                        DoStartGame();
                        break;
                    }
                default:
                    {

                        break;
                    }
            }
        }

        private void HandlerDestroyBalls(int x, int y, Color playBallColor)
        {
            HandlerSameColorBalls(x, y, playBallColor);
            HandlerIslands();

            DestroyBalls();

            HandlerGameComplete();
        }

        private void HandlerSameColorBalls(int x, int y, Color playBallColor)
        {
            //Вызов этого метода тут - костыль для цикла for. При цикле do while будет другой костыль - проверка, что список не пустой, По этому особой особой нет
            CheckNearbyBalls(x, y, playBallColor);

            int i, listCount;
            listCount = DestroyBallsList.Count;

            for (i = 0; i < listCount; i++)
            {
                x = DestroyBallsList[i].x;
                y = DestroyBallsList[i].y;

                CheckNearbyBalls(x, y, playBallColor);

                listCount = DestroyBallsList.Count;
            }
            if (DestroyBallsList.Count >= 3)
            {
                DestroyBalls();
            }
            else
            {
                DestroyBallsList.Clear();
            }
        }
        private void CheckNearbyBalls(int x, int y, Color playBallColor)
        {
            Ball ball;

            if (y > 0 && GetColor(x, y - 1) == playBallColor)
            {
                ball = new Ball(x, y - 1);
                if (CheckOnUniqueBallInList(ball, DestroyBallsList))
                {
                    DestroyBallsList.Add(ball);
                }
            }
            //Проверка снизу
            if (y < _LimitRowsBalls && GetColor(x, y + 1) == playBallColor)
            {
                ball = new Ball(x, y + 1);
                if (CheckOnUniqueBallInList(ball, DestroyBallsList))
                {
                    DestroyBallsList.Add(ball);
                }
            }
            //Проверка справа
            if (x < _BoxWidth - 1 && GetColor(x + 1, y) == playBallColor)
            {
                ball = new Ball(x + 1, y);
                if (CheckOnUniqueBallInList(ball, DestroyBallsList))
                {
                    DestroyBallsList.Add(ball);
                }
            }
            //Проверка слева
            if (x > 0 && GetColor(x - 1, y) == playBallColor)
            {
                ball = new Ball(x - 1, y);
                if (CheckOnUniqueBallInList(ball, DestroyBallsList))
                {
                    DestroyBallsList.Add(ball);
                }
            }
        }

        private bool CheckOnUniqueBallInList(Ball ball, List<Ball> listBalls)
        {
            for (int i = 0; i < listBalls.Count; i++)
            {
                if (listBalls[i].Equals(ball))
                {
                    return false;
                }
            }

            return true;
        }

        public void DestroyBalls()
        {
            Ball ball;

            for (int i = 0; i < DestroyBallsList.Count; i++)
            {
                ball = DestroyBallsList[i];

                IncrementScore();

                SetColor(ball.x, ball.y, Color.Empty);
            }
            DestroyBallsList.Clear();

            System.Windows.Forms.Application.DoEvents();
        }

        private void IncrementScore()
        {
            Score += _ScorePerBall;
        }

        // Будет для каждого шарика проверять, есть ли у него основание
        private void HandlerIslands()
        {
            Ball ball;

            for (int i = _LimitRowsBalls - 1; i >= 0; i--)
            {
                for (int j = 0; j < _BoxWidth; j++)
                {
                    //проверка на то, что шарик является островом
                    if (CheckOnOneBallIsIsland(j, i))
                    {
                        ball = new Ball(j, i);
                        //MessageBox.Show(j.ToString() + " | " + i, ToString());
                        DestroyBallsList.Add(ball);
                    }
                }
            }
        }

        private void HandlerGameComplete()
        {
            if (CheckOnEmptyPlayField()) { EndGame(); }
        }

        private bool CheckOnEmptyPlayField()
        {
            for (int i = 0; i < _LimitRowsBalls; i++)
            {
                for (int j = 0; j < _BoxWidth; j++)
                {
                    if (GetColor(j, i) != Color.Empty) { return false; }
                }
            }

            return true;
        }

        //Вызывает метод волновой проверки для одного шарика
        private bool CheckOnOneBallIsIsland(int x, int y)
        {
            Array.Clear(Island_Array, 0, Island_Array.Length);
            //Проверка на наличие основания у проверяемого шарика. если вернёт true, то это неостров
            if (GetColor(x, y) != Color.Empty) { return !CheckOnHasGround(x, y); }

            return false;
        }

        //Истина - имеется основание. Ложь - отсутствует
        private bool CheckOnHasGround(int x, int y)
        {
            //Две проверки на крайние позиции. При таких значениях шарик сам является основанием, и нет смысла проверять его дальше.

            if (y == 0)
            {
                return true;
            }

            //Заполняю массив для формирования волны для одного шарика
            CreateWave(x, y);

            //Проверка что волна дошла до крайних ячеек, то есть, имеется основание; истина - имеется. Ложь - отсутствует
            return CheckOnHaveExtrimWaveCells();
        }

        //Создать волну для одного шарика
        private void CreateWave(int x, int y)
        {
            int level = 1;
            Island_Array[y, x] = level;

            List<Ball> tempList = new List<Ball>();
            Ball ball;


            //обработать соседние шарики от главного
            CreateOneLevel(x, y, ref tempList);
            int listCount = tempList.Count;

            for (int i = 0; i < listCount; i++)
            {
                ball = tempList[i];
                CreateOneLevel(ball.x, ball.y, ref tempList);

                listCount = tempList.Count;
            }
        }

        private void CreateOneLevel(int x, int y, ref List<Ball> tempList)
        {
            Ball ball;
            if (y > 0 && GetColor(x, y - 1) != Color.Empty)
            {
                if (Island_Array[y - 1, x] == 0)
                {
                    Island_Array[y - 1, x] = Island_Array[y, x] + 1;
                    ball = new Ball(x, y - 1);
                    tempList.Add(ball);
                }
            }
            if (y < _LimitRowsBalls - 1 && GetColor(x, y + 1) != Color.Empty)
            {
                if (Island_Array[y + 1, x] == 0)
                {
                    Island_Array[y + 1, x] = Island_Array[y, x] + 1;
                    ball = new Ball(x, y + 1);
                    tempList.Add(ball);
                }
            }
            if (x > 0 && GetColor(x - 1, y) != Color.Empty)
            {
                if (Island_Array[y, x - 1] == 0)
                {
                    Island_Array[y, x - 1] = Island_Array[y, x] + 1;
                    ball = new Ball(x - 1, y);
                    tempList.Add(ball);
                }
            }
            if (x < _BoxWidth - 1 && GetColor(x + 1, y) != Color.Empty)
            {
                if (Island_Array[y, x + 1] == 0)
                {
                    Island_Array[y, x + 1] = Island_Array[y, x] + 1;
                    ball = new Ball(x + 1, y);
                    tempList.Add(ball);
                }
            }

        }

        //Если основание есть, то истина, иначе ложь
        private bool CheckOnHaveExtrimWaveCells()
        {
            for (int j = 0; j < _BoxWidth; j++)
            {
                if (Island_Array[0, j] != 0) { return true; }
            }

            return false;
        }

        //Обновляет горизонтальное положение шарика в соответствии с положением курсора.
        public void MouseHandlerPlayBall(int x)
        {
            ColPlayBall = (x - XPlayField) / _BallSize;
            XPlayBall = XPlayBall;

            Invalidate();
        }

        private void SetColor(int x, int y, Color ball)
        {
            Balls_Array[y, x] = ball;
            Invalidate();
        }

        //Возвращает цвет элемента массива Box
        private Color GetColor(int x, int y)
        {
            return Balls_Array[y, x];
        }

        private Color GetRandomColor(int quantityColors)
        {
            UpdateStoreColors();

            Color color = Color.Empty;
            int digit;
            do
            {
                digit = random.Next(quantityColors);
            } while (StoreColors[digit] == Color.Transparent);

            color = StoreColors[digit];

            return color;
        }

        private void UpdateStoreColors()
        {
            StoreColors[0] = FirstBallColor;
            StoreColors[1] = SecondBallColor;
            StoreColors[2] = ThirdBallColor;
            StoreColors[3] = FourthBallColor;
            StoreColors[4] = FifthBallColor;
        }



        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = 0x02000000;
                return cp;
            }
        }
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, height, specified);

            height = _BallSize * _BoxWidth;

            WidthPlayField = _widthPlayField;
            HeightPlayField = _heightPlayField;

            XPlayField = x;
            YPlayFiled = y;

            Invalidate();

        }

        //События

        //Событие: игра началась
        public event EventHandler OnStartGame
        {
            add
            {
                _onStartGame += value;
            }
            remove
            {
                _onStartGame -= value;
            }

        }
        public void DoStartGame()
        {
            _onStartGame?.Invoke(this, new EventArgs());
        }

        //Событие: изменение счёта
        public event EventHandler OnScoreChanged
        {
            add { _onScoreChanged += value; }
            remove { _onScoreChanged -= value; }
        }
        public void DoScoreChanged()
        {
            _onScoreChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler OnEndGame
        {
            add { _onEndGame += value; }
            remove { _onEndGame -= value; }
        }

        public void DoEndGame()
        {
            _onEndGame?.Invoke(this, new EventArgs());
        }
    }
}