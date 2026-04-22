namespace WinFormsComponents.Classes.Model
{
    [Serializable]
    /// <summary>
    /// Объект арки для загрузчика
    /// </summary>
    public class ArcLoaderElement
    {
        /// <summary>
        /// Угол смещения
        /// </summary>
        private int angle = 0;

        /// <summary>
        /// Отступ
        /// </summary>
        public int Padding { get; set; }
        /// <summary>
        /// Ширина
        /// </summary>
        public int Thickness { get; set; }
        /// <summary>
        /// Цвет
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// Угол арки в градусах (360 - круг)
        /// </summary>
        public int Arc { get; set; }
        /// <summary>
        /// Скорость вращения
        /// </summary>
        public int Speed { get; set; }
        /// <summary>
        /// Сторона вращения
        /// </summary>
        public ToolStripItemAlignment SideOfRotation { get; set; }

        /// <summary>
        /// Угол смещения
        /// </summary>
        public int Angle => angle;

        /// <summary>
        /// Объект арки для загрузчика
        /// </summary>
        /// <param name="padding">Отступ</param>
        /// <param name="thickness">Ширина</param>
        /// <param name="color">Цвет</param>
        /// <param name="arc">Угол арки в градусах (360 - круг)</param>
        /// <param name="speed">Скорость вращения</param>
        /// <param name="sideOfRotation">Направление вращения</param>
        public ArcLoaderElement(int padding, int thickness, Color color, int arc, int speed, ToolStripItemAlignment sideOfRotation)
        {
            Padding = padding;
            Thickness = thickness;
            Color = color;
            Arc = arc;
            Speed = speed;
            SideOfRotation = sideOfRotation;
        }

        /// <summary>
        /// Перекрас рисовщика, параметрами арки
        /// </summary>
        /// <param name="pen">Рисовщик</param>
        public void RepaintPen(Pen pen)
        { 
            pen.Width = Thickness;
            pen.Color = Color;
        }

        /// <summary>
        /// Обновление угла смещения
        /// </summary>
        public void UpdateAngle() => angle = (angle + (SideOfRotation == ToolStripItemAlignment.Left ? -Speed : Speed)) % 360;

        /// <summary>
        /// Установка размера арки, относительно изначального размера
        /// </summary>
        /// <param name="baseSize">Первоначальный размер</param>
        public void Resize(ref int baseSize)
        {
            baseSize -= Thickness * Padding;
        }

        /// <summary>
        /// Создание случайной арки
        /// </summary>
        /// <returns></returns>
        public static ArcLoaderElement RandomArc()
        {
            Random random = new ();

            return new(
                        random.Next(2,7), 
                        random.Next(2, 7),
                        GetNotTransperentKeyColor(), 
                        random.Next(215, 315), 
                        random.Next(2, 10), 
                        Convert.ToBoolean(random.Next(0, 1)) ? ToolStripItemAlignment.Left : ToolStripItemAlignment.Right
                      );
        }

        /// <summary>
        /// Получение списка рандомных арок
        /// </summary>
        /// <returns>Список арок</returns>
        public static IEnumerable<ArcLoaderElement> GetArcElementsRandomCollection()
        {
            int randomCount = new Random().Next(3, 7);

            for (int i = 0; i < randomCount; i++)
            {
                yield return ArcLoaderElement.RandomArc();
            }
        }

        private static Color GetNotTransperentKeyColor()
        {
            Color color = Extensions.RandomColor();

            while (color == Color.Gray || color == Color.Transparent)
            { 
                color = Extensions.RandomColor();
            }

            return color;
        }
    }
}
