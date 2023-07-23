using System.Globalization;

using OxyPlot;
using OxyPlot.Series;

using SharpEval.Core.Maths.Sequences;

namespace SharpEval.Core.Internals
{
    /// <summary>
    /// Plot delegate
    /// </summary>
    /// <param name="argument"></param>
    /// <returns></returns>
    public delegate double PlotDelegate(double argument);

    internal sealed class Plotter
    {
        private readonly PlotModel _model;
        private int _width;
        private int _height;

        public Plotter()
        {
            _model = new PlotModel
            {
                Background = OxyColor.FromRgb(255, 255, 255),
                Culture = CultureInfo.InvariantCulture,
            };
            _width = 1800;
            _height = 1200;
        }

        public void Reset()
        {
            _model.Background = OxyColor.FromRgb(255, 255, 255);
            _model.Culture = CultureInfo.InvariantCulture;
            _model.Series.Clear();
            _width = 1800;
            _height = 1200;
        }

        public void Title(string title)
        {
            _model.Title = title;
        }

        public void Background(string htmlColor)
        {
            _model.Background = OxyColor.Parse(htmlColor);
        }

        public void Function(Func<double, double> function, double start, double end, double step, string title)
        {
            _model.Series.Add(new FunctionSeries(function, start, end, step, title));
        }

        public void Line(NumberSequenceBase sequence)
        {
            var series = new LineSeries();
            series.Points.AddRange(sequence.Select((x, y) => new DataPoint(x, y)));
            _model.Series.Add(series);
        }

        public void Size(int width, int height) 
        {
            if (width < 1)
                throw new ArgumentException("Width must be at least 1 pixel");

            if (height < 1)
                throw new ArgumentException("Height must be at least 1 pixel");

            _width = width;
            _height = height;
        }

        public ISvgImage Plot()
        {
            var exporter = new SvgExporter
            {
                Width = _width,
                Height = _height,
            };
            return new SvgImage
            {
                Data = exporter.ExportToString(_model)
            };
        }
    }
}
