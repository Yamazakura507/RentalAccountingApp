using System.ComponentModel;
using System.Drawing.Drawing2D;
using WinFormsComponents.Classes;
using WinFormsComponents.Classes.Model;

namespace WinFormsComponents.Controls
{
    public partial class Loader : UserControl
    {
        private bool isAnimating = false;

        /// <summary>
        /// Массив арок
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ArcLoaderElement[] ArcElements { get; set; }

        /// <summary>
        /// Интервал обновления таймера
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int RotationSpeed { get; set; } = 30;

        public Loader()
        {
            InitializeComponent();

            tAnimation.Interval = RotationSpeed;
        }

        private void tAnimationOnTick(object sender, EventArgs e)
        {
            if (ArcElements == null) return;

            foreach (ArcLoaderElement arc in ArcElements)
            {
                arc.UpdateAngle();
            }

            this.Invalidate();
        }

        private void LoaderOnPaint(object sender, PaintEventArgs e)
        {
            if (ArcElements == null) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int size = Math.Min(this.Width, this.Height);

            using (Pen pen = new(Color.Transparent))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                foreach (ArcLoaderElement arc in ArcElements)
                {
                    DrawArc(e, pen, ref size, arc);
                }
            }
        }

        /// <summary>
        /// Запустить анимацию
        /// </summary>
        public void StartAnimation()
        {
            if (!isAnimating)
            {
                this.Parent.InterfaceLock(this);
                isAnimating = true;
                tAnimation.Start();
            }
        }

        /// <summary>
        /// Остановить анимацию
        /// </summary>
        public void StopAnimation()
        {
            if (isAnimating)
            {
                isAnimating = false;
                tAnimation.Stop();
                this.Parent.InterfaceUnlock(this);
            }
        }

        /// <summary>
        /// Рисовка арки
        /// </summary>
        /// <param name="e">Событие перерисовки</param>
        /// <param name="pen">Рисовщик</param>
        /// <param name="size">Базовый размер</param>
        /// <param name="arc">Объект арки</param>
        private void DrawArc(PaintEventArgs e, Pen pen, ref int size, ArcLoaderElement arc)
        {
            arc.RepaintPen(pen);
            arc.Resize(ref size);

            Rectangle rect = new((this.Width - size) / 2, (this.Height - size) / 2, size, size);
            e.Graphics.DrawArc(pen, rect, arc.Angle, arc.Arc);
        }

        /// <summary>
        /// Авто настройка загрузчика
        /// </summary>
        /// <param name="control">Родительский элемент</param>
        public void AutoSetup(Control control)
        {
            this.ArcElements = ArcLoaderElement.GetArcElementsRandomCollection().ToArray();
            this.Location = new((control.Width - this.Width) / 2, (control.Height - this.Height) / 2);
            control.Controls.Add(this);
            this.BringToFront();

            control.Resize += (s, e) => this.Location = new((control.Width - this.Width) / 2, (control.Height - this.Height) / 2);
        }
    }
}
