using OxyPlot;
using OxyPlot.Series;

using SharpEval.Core.Internals;

namespace SharpEval.Core
{

    /// <summary>
    /// Allows plotting data series
    /// </summary>
    public sealed class Plotter
    {
        private readonly PlotModel _model;

        /// <summary>
        /// Creates a new instance of plotter
        /// </summary>
        public Plotter()
        {
            _model = new PlotModel
            {
                Background = OxyColor.FromRgb(255, 255, 255)
            };
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
        /// Plot the image
        /// </summary>
        /// <returns>An ISvgImage that can be plotted</returns>
        public ISvgImage Plot()
        {
            var exporter = new SvgExporter
            {
                Width = 1800,
                Height = 1200,
            };
            return new SvgImage
            {
                Data = exporter.ExportToString(_model)
            };
        }
    }
}
