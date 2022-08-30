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
        private int _ballSize;
        private bool _isStartGame;
        private bool _isNotifyToGameComplete;

        private Color _colorPlayFieldFrame;
        private Color _colorFillPlayField;
        private Color _colorDefeatLine;

        private Color _firstBallColor;
        private Color _secondBallColor;
        private Color _thirdBallColor;
        private Color _fourthBallColor;
        private Color _fifthBallColor;

        //Внутренние поля без публичных свойств

        private int ballStartY;//Относительно игрового поля

        public bool isBallFlying;

        //Глобальные объекты

        Brush brushPlayBall = new SolidBrush(Color.Empty);

        //Константы

        const int _BoxWidth = 10;
        const int _BoxHeight = 25;
        const int _StartQuantityBalls = 15;
        const int _LimitRowsBalls = 20;
        const int _LimitQuantityColors = 5;
        const int _BallStep = 1;
        const int _ScorePerBall = 1;
        const int _MinBallSize = 10;
        const int _MaxBallSize = 40;

        //Массивы

        Color[,] Balls_Array = new Color[_BoxHeight, _BoxWidth];
        Color[] StoreColors = new Color[5];
        int[,] Island_Array = new int[_LimitRowsBalls, _BoxWidth];
        List<Ball> DestroyBallsList = new List<Ball>(_BoxWidth * _LimitRowsBalls);


        //События

        private event EventHandler _onStartGame;
        private event EventHandler _onScoreChanged;
        private event EventHandler _onEndGame;

        //Конструктор класса

        public CourseProject()
        {
            IsNotifyToGameComplete = false;
            isBallFlying = false;
            BallSize = 20;
            StartGame();

        }

        
        #region свойства int
        private int XPlayField
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

        private int YPlayFiled
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

        private int WidthPlayField
        {
            get => _widthPlayField;
            set
            {
                value = BallSize * _BoxWidth;

                if (_widthPlayField != value)
                {
                    _widthPlayField = value;

                    Invalidate();
                }
            }
        }

        private int HeightPlayField
        {
            get => _heightPlayField;
            set
            {
                value = BallSize * _BoxHeight;

                if (_heightPlayField != value)
                {
                    _heightPlayField = value; Invalidate();
                }
            }
        }

        private int XPlayBall
        {
            get => _xPlayBall;
            set
            {
                value = XPlayField + BallSize * ColPlayBall;
                if (value > XPlayField + WidthPlayField) { value = XPlayField + WidthPlayField; }
                if (value < XPlayField) { value = XPlayField; }
                if (_xPlayBall != value) { _xPlayBall = value; Invalidate(); }
            }
        }

        private int YPlayBall
        {
            get => _yPlayBall;
            set
            {
                if (value > YPlayFiled + HeightPlayField - BallSize) { value = YPlayFiled + HeightPlayField - BallSize; }
                if (value < 0) { value = 0; }
                if (_yPlayBall != value) { _yPlayBall = value; Invalidate(); }
            }
        }

        private int XDefeatLine
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

        private int YDefeatLine
        {
            get => _yDefeatLine;
            set
            {
                value = YPlayFiled + BallSize * _LimitRowsBalls;
                if (_yDefeatLine != value)
                {
                    _yDefeatLine = value; Invalidate();
                }
            }
        }

        private int ColPlayBall
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
        }

        private int BallSize
        {
            get => _ballSize;
            set
            {
                value = BallSize;
                if (value * _BoxWidth > Width | value * _BoxHeight > Height)
                {
                    value = value / 2;
                }
                if ((value * 2 * _BoxWidth <= Width) && (value * 2 * _BoxHeight <= Height))
                {
                    value = value * 2;
                }


                if (value < _MinBallSize) { value = _MinBallSize; }
                if (value > _MaxBallSize) { value = _MaxBallSize; }
                if (_ballSize != value) { _ballSize = value; Invalidate(); }
            }
        }
        #endregion
        public bool IsNotifyToGameComplete
        {
            get => _isNotifyToGameComplete;
            set { if (_isNotifyToGameComplete != value) { _isNotifyToGameComplete = value; } }
        }

        public bool IsStartGame
        {
            get => _isStartGame;
            //set { if (_isStartGtame != value) { _isStartGtame = value; Invalidate(); if (value) { DoStartGame(); } else { DoEndGame(); } } }
        }

        public Color ColorPlayFieldFrame
        {
            get => _colorPlayFieldFrame;

            set
            {
                if (_colorPlayFieldFrame != value) { _colorPlayFieldFrame = value; Invalidate(); }
            }
        }

        public Color ColorFillPlayField
        {
            get => _colorFillPlayField;
            set { if (_colorFillPlayField != value) { _colorFillPlayField = value; Invalidate(); } }
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
            set { if (value == Color.Empty) { value = Color.Transparent; } if (_firstBallColor != value) { _firstBallColor = value; Invalidate(); } }
        }
        public Color SecondBallColor
        {
            get => _secondBallColor;
            set
            {
                if (value == Color.Empty) { value = Color.Transparent; }
                if (_secondBallColor != value) { _secondBallColor = value; Invalidate(); }
            }
        }
        public Color ThirdBallColor
        {
            get => _thirdBallColor;
            set { if (value == Color.Empty) { value = Color.Transparent; } if (_thirdBallColor != value) { _thirdBallColor = value; Invalidate(); } }
        }
        public Color FourthBallColor
        {
            get => _fourthBallColor;
            set { if (value == Color.Empty) { value = Color.Transparent; } if (_fourthBallColor != value) { _fourthBallColor = value; Invalidate(); } }
        }
        public Color FifthBallColor
        {
            get => _fifthBallColor;
            set { if (value == Color.Empty) { value = Color.Transparent; } if (_fifthBallColor != value) { _fifthBallColor = value; Invalidate(); } }
        }

        //Метод для отрисовки содержимого компонента
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen penPlayField = new Pen(ColorPlayFieldFrame);
            Pen penDefeatLine = new Pen(ColorPlayFieldFrame);
            Brush brushFillPlayField = new SolidBrush(ColorFillPlayField);

            e.Graphics.FillRectangle(brushFillPlayField, XPlayField, YPlayFiled, WidthPlayField, HeightPlayField);
            //Игровое поле
            e.Graphics.DrawRectangle(penPlayField, XPlayField, YPlayFiled, WidthPlayField, HeightPlayField);
            //Граница поражения
            e.Graphics.DrawLine(penDefeatLine, XDefeatLine, YDefeatLine, XDefeatLine + WidthPlayField, YDefeatLine);
            if (IsStartGame)
            {
                //Отрисовка существующих шариков
                Brush ball;
                for (int i = 0; i < _BoxHeight; i++)
                {
                    for (int j = 0; j < _BoxWidth; j++)
                    {
                        ball = new SolidBrush(GetColor(j, i));
                        e.Graphics.FillEllipse(ball, BallSize * j + XPlayField, YPlayFiled + BallSize * i, BallSize, BallSize);
                    }
                }
                //Управляемый шарик
                e.Graphics.FillEllipse(brushPlayBall, XPlayBall, YPlayBall, BallSize, BallSize);

                //Удаление кисти
                penPlayField.Dispose();
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            }
        }
        //Метод начинает игру
        public void StartGame()
        {
            SetStartGameValues();
            ClearBalls_Array();
            MixBalls();
            CreatePlayBall();

            _isStartGame = true;
            DoStartGame();
            Select();
        }
        //Метод заканчивает игру
        public void EndGame()
        {
            ClearBalls_Array();
            _isStartGame = false;
            DoEndGame();
        }
        //Метод очищает массив шариков
        private void ClearBalls_Array()
        {
            for (int i = 0; i < _BoxHeight; i++)
            {
                for (int j = 0; j < _BoxWidth; j++)
                {
                    Balls_Array[i, j] = Color.Empty;
                }
            }
        }
        //Метод для настройки свойств и полей перед началом игру
        private void SetStartGameValues()
        {
            WidthPlayField = BallSize * _BoxWidth;
            HeightPlayField = BallSize * _BoxHeight;

            XPlayField = (Width / 2) - WidthPlayField / 2;
            YPlayFiled = (Height / 2) - HeightPlayField / 2;

            ColPlayBall = (_BoxWidth / 2) - 1;

            XPlayBall = XPlayField + BallSize * _colPlayBall;
            YPlayBall = YPlayFiled + HeightPlayField - BallSize;

            XDefeatLine = XDefeatLine;
            YDefeatLine = YDefeatLine;

            ballStartY = YPlayBall;


            ResetScore();
        }

        //Метод обнуляет свойство Score
        private void ResetScore() { _score = 0; DoScoreChanged(); }

        //Метод задаёт каждому шарику в массиве случайных цвет из набора цветов
        private void MixBalls()
        {
            for (int i = 0; i < _StartQuantityBalls; i++)
            {
                for (int j = 0; j < _BoxWidth; j++)
                {
                    SetColor(j, i, GetRandomColor(_LimitQuantityColors));
                }
            }
            Invalidate();
        }

        //Меняет цвет игрового шарика и ставит его в начальную позицию по оси Y
        private void CreatePlayBall()
        {
            brushPlayBall = new SolidBrush(GetRandomColor(_LimitQuantityColors));

            YPlayBall = ballStartY;
        }

        //Запускает игровой шарик вверх и обрабатывает его полёт
        public void LaunchPlayBall()
        {
            isBallFlying = true;

            int row;

            do
            {
                row = (YPlayBall - YPlayFiled) / BallSize;
                YPlayBall -= _BallStep;

                if (CheckOnMultipleRow())
                {
                    if (CheckOnNeedStopPlayBall(row))
                    {
                        StopPlayBall(row);
                    }

                    Thread.Sleep(10);
                    Application.DoEvents();
                }
            }
            while (isBallFlying);
        }

        //Проверка значения высоты управляемого шарика на кратность высоты какого-либо элемента массива шариков
        private bool CheckOnMultipleRow()
        {
            if ((YPlayBall - YPlayFiled) % BallSize == 0)
            {
                return true;
            }
            return false;
        }

        //Проверка на необходимость остановки управляемого шарика
        private bool CheckOnNeedStopPlayBall(int row)
        {
            if ((row > 0 && GetColor(ColPlayBall, row - 1) != Color.Empty) || row == 0)
            {
                return true;
            }

            return false;
        }

        //Остановка управляемого шарика
        private void StopPlayBall(int row)
        {
            Color playBallColor = ((SolidBrush)(brushPlayBall)).Color;

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
            isBallFlying = false;
        }

        //Проверка на поражение в игре
        private bool CheckOnDefeat()
        {
            if (YPlayBall >= YDefeatLine)
            {
                return true;
            }

            return false;
        }

        //Метод обрабатывающий поражение в игре
        private void Defeat()
        {
            DialogResult dialogResult = MessageBox.Show("Начать заного?", "Поражение", MessageBoxButtons.YesNo);
            switch (dialogResult)
            {
                case DialogResult.Yes:
                    {
                        StartGame();
                        break;
                    }
                default:
                    {
                        EndGame();
                        break;
                    }
            }
        }

        //Метод обрабатывает все поиски и удаления шариков
        private void HandlerDestroyBalls(int x, int y, Color playBallColor)
        {
            HandlerSameColorBalls(x, y, playBallColor);
            HandlerIslands();
            HandlerGameComplete();
        }

        //Метод ищет и удаляет все соседние шарики одного цвета с управляемым при его столкновении с ними
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

        //Метод проверяет соседние шарики для каждого проверяемого на идентичность цвета
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

        //Метод проверяет найденный подходящий шарик на то, если тот уже в списке
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

        //Метод удаляет все шарики из массива шариков, которые есть в списке для удаления
        private void DestroyBalls()
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

        //Метод увеличивает значение поля _score
        private void IncrementScore()
        {
            _score += _ScorePerBall;
            DoScoreChanged();
        }

        // Метод проверяет каждый шарик на наличие у него "основания"
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
            DestroyBalls();
        }

        //Метод обрабатывает победу в игре
        private void HandlerGameComplete()
        {
            if (CheckOnEmptyPlayField())
            {
                DialogResult dialogResult = MessageBox.Show("Начать заного?", "Победа", MessageBoxButtons.YesNo);
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        {
                            StartGame();
                            break;
                        }
                    default:
                        {
                            EndGame();
                            break;
                        }
                }
            }
        }

        //Метод проверяет игровое поле на пустоту
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
            //Проверка на крайние позиции. При таких значениях шарик сам является основанием, и нет смысла проверять его дальше.
            if (y == 0)
            {
                return true;
            }
            //Формирование волны для одного шарика
            CreateWave(x, y);

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

        //Находит соседние, вплотную стоящие, шарики к проверяемому
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

        //Проверяет сформированную волну, дошла ли она до основания игрового поля
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
            if (IsStartGame && !isBallFlying)
            {
                ColPlayBall = (x - XPlayField) / BallSize;
                XPlayBall = XPlayBall;
                Invalidate();
            }
        }

        //Изменяет значение элемента массива шариков
        private void SetColor(int x, int y, Color ball)
        {
            Balls_Array[y, x] = ball;
            Invalidate();
        }

        //Возвращает, как результат цвет элемента массива шариков
        private Color GetColor(int x, int y)
        {
            return Balls_Array[y, x];
        }

        //Возвращает, как результат случайный цвет из набора цветов
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

        //Обновлет содержимое массива хранящего значения всех цветов из набора
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

        //Метод отвечает за изменене размеров компонента при его измеении со стороны пользователя
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, height, specified);

            BallSize = BallSize;
            WidthPlayField = _widthPlayField;
            HeightPlayField = _heightPlayField;
            XPlayField = XPlayField;
            YPlayFiled = YPlayFiled;
            XPlayBall = XPlayBall;
            YPlayBall = YPlayBall;
            XDefeatLine = XDefeatLine;
            YDefeatLine = YDefeatLine;

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

        //Событие: Игра закончилась
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