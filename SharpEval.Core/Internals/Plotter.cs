using OxyPlot;
using OxyPlot.Series;

namespace SharpEval.Core.Internals
{

    /// <summary>
    /// Allows plotting data series
    /// </summary>
    public sealed class Plotter
    {
        private readonly PlotModel _model;
        private int _width;
        private int _height;

        /// <summary>
        /// Creates a new instance of plotter
        /// </summary>
        public Plotter()
        {
            _model = new PlotModel
            {
                Background = OxyColor.FromRgb(255, 255, 255)
            };
            _width = 1800;
            _height = 1200;
        }

        /// <summary>
        /// Set plot title
        /// </summary>
        /// <param name="title">Plot title</param>
        /// <returns>the current instance</returns>
        public Plotter Title(string title)
        {
            _model.Title = title;
            return this;
        }

        /// <summary>
        /// Set the plot background
        /// </summary>
        /// <param name="htmlColor">A color in html format. E.g: #ffffff</param>
        /// <returns>the current instance</returns>
        public Plotter Background(string htmlColor)
        {
            _model.Background = OxyColor.Parse(htmlColor);
            return this;
        }

        /// <summary>
        /// Plot a function
        /// </summary>
        /// <param name="function">function to plot</param>
        /// <param name="start">start value</param>
        /// <param name="end">end value</param>
        /// <param name="step">step</param>
        /// <param name="title">function title</param>
        /// <returns>the current instance</returns>
        public Plotter Function(Func<double, double> function, double start, double end, double step, string title)
        {
            _model.Series.Add(new FunctionSeries(function, start, end, step, title));
            return this;
        }

        /// <summary>
        /// Sets the plot area size
        /// </summary>
        /// <param name="width">Width. Must be at least 1</param>
        /// <param name="height">Height. Must be at least 1</param>
        /// <returns>the current instance</returns>
        /// <exception cref="ArgumentException">When width or height is smaller than 1</exception>
        public Plotter Size(int width, int height) 
        {
            if (width < 1)
                throw new ArgumentException("Width must be at least 1 pixel");

            if (height < 1)
                throw new ArgumentException("Height must be at least 1 pixel");

            _width = width;
            _height = height;

            return this;
        }

        /// <summary>
        /// Plot the image
        /// </summary>
        /// <returns>An ISvgImage that can be plotted</returns>
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
