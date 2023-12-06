﻿using Avalonia;
using Avalonia.Media;

#nullable enable

namespace Projektanker.Icons.Avalonia
{
    public class IconImage : AvaloniaObject, IImage
    {
        public static readonly StyledProperty<string?> ValueProperty =
            AvaloniaProperty.Register<IconImage, string?>(nameof(Value));

        public static readonly StyledProperty<IBrush?> BrushProperty =
            AvaloniaProperty.Register<IconImage, IBrush?>(nameof(Brush));

        public static readonly StyledProperty<IPen?> PenProperty =
            AvaloniaProperty.Register<IconImage, IPen?>(nameof(Pen));

        private Rect _bounds;
        private GeometryDrawing? _drawing;

        public IconImage() : this(string.Empty, new SolidColorBrush(Colors.Black))
        {
        }

        public IconImage(string? value, IBrush? brush)
        {
            Value = value;
            Brush = brush;
            HandleValueChanged();
            HandleBrushChanged();
        }

        public string? Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public IBrush? Brush
        {
            get => GetValue(BrushProperty);
            set => SetValue(BrushProperty, value);
        }

        public IPen? Pen
        {
            get => GetValue(PenProperty);
            set => SetValue(PenProperty, value);
        }

        /// <inheritdoc>
        public Size Size => _bounds.Size;

        /// <inheritdoc/>
        void IImage.Draw(
            DrawingContext context,
            Rect sourceRect,
            Rect destRect)
        {
            var drawing = _drawing;
            if (drawing == null)
            {
                return;
            }

            var bounds = _bounds;
            var scale = Matrix.CreateScale(
                destRect.Width / sourceRect.Width,
                destRect.Height / sourceRect.Height);
            var translate = Matrix.CreateTranslation(
                -sourceRect.X + destRect.X - bounds.X,
                -sourceRect.Y + destRect.Y - bounds.Y);

            using (context.PushClip(destRect))
            using (context.PushTransform(translate * scale))
            {
                drawing.Draw(context);
            }
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);
            if (change.Property == ValueProperty)
            {
                HandleValueChanged();
            }
            else if (change.Property == BrushProperty)
            {
                HandleBrushChanged();
            }
            else if (change.Property == PenProperty)
            {
                HandlePenChanged();
            }
        }

        private void HandleValueChanged()
        {
            var iconModel = IconProvider.Current.GetIcon(Value);

            _bounds = new Rect(
                x: iconModel.ViewBox.X,
                y: iconModel.ViewBox.Y,
                width: iconModel.ViewBox.Width,
                height: iconModel.ViewBox.Height);

            var drawing = GetGeometryDrawing();
            drawing.Geometry = StreamGeometry.Parse(iconModel.Path);
        }

        private void HandleBrushChanged()
        {
            var drawing = GetGeometryDrawing();
            drawing.Brush = Brush;
        }

        private void HandlePenChanged()
        {
            var drawing = GetGeometryDrawing();
            drawing.Pen = Pen;
        }

        private GeometryDrawing GetGeometryDrawing()
        {
            return _drawing ??= new GeometryDrawing();
        }
    }
}
