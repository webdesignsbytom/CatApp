using CatApp.ViewModel.Games;
using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace CatApp.View.Games;

public partial class GamesPage : ContentPage
{
	public GamesPageViewModel ViewModel { get; set; }

    // Game swipe
    private Point _lastPoint;
    private DateTime _lastTime;
    private bool _isTracking = false;
    private Direction _lastDirection = Direction.None;
    // Min movement speed
    private const double MinSwipeDistance = 10.0;

    public GamesPage(GamesPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = ViewModel = viewModel;
        canvasView.Touch += CanvasView_Touch;
    }

    private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        SKImageInfo info = e.Info;
        SKSurface surface = e.Surface;
        var canvas = surface.Canvas; // Assign the canvas to the property

        canvasView.IgnorePixelScaling = true;
        canvas.Clear(SKColors.Red);

        ViewModel.GameLoop(canvas);
        // Invalidate the canvas to cause a redraw
        canvasView.InvalidateSurface();
    }

    private void CanvasView_Touch(object sender, SKTouchEventArgs e)
    {
        switch (e.ActionType)
        {
            case SKTouchAction.Pressed:
                // Start tracking
                _lastPoint = new Point(e.Location.X, e.Location.Y);
                _lastTime = DateTime.Now;
                _isTracking = true;
                _lastDirection = Direction.None;
                break;

            case SKTouchAction.Moved:
                if (_isTracking)
                {
                    // Calculate the distance moved
                    var currentPoint = new Point(e.Location.X, e.Location.Y);
                    var distance = Math.Sqrt(Math.Pow(currentPoint.X - _lastPoint.X, 2) + Math.Pow(currentPoint.Y - _lastPoint.Y, 2));

                    // Only consider it a swipe if the distance moved is greater than the threshold
                    if (distance > MinSwipeDistance)
                    {
                        var currentDirection = GetDirection(_lastPoint, currentPoint);

                        if (_lastDirection != Direction.None && currentDirection != _lastDirection)
                        {
                            var currentTime = DateTime.Now;
                            var timeDiff = (currentTime - _lastTime).TotalMilliseconds;

                            // Logging for demonstration, replace with your actual logic
                            Console.WriteLine("Movement detected with direction change.");

                            // Example: Detect quick back and forth motion
                            if (timeDiff < 150)
                            {
                                Console.WriteLine("XXX Scratching back-and-forth motion detected!");
                            }                           
                            else if (timeDiff < 300)
                            {
                                Console.WriteLine("AAA Scratching back-and-forth motion detected!");
                            }
                            else if (timeDiff < 600)
                            {
                                Console.WriteLine("BBB Scratching back-and-forth motion detected!");
                            }                            
                            else if (timeDiff < 900)
                            {
                                Console.WriteLine("CCC Scratching back-and-forth motion detected!");
                            }

                            _lastTime = currentTime;
                        }

                        _lastPoint = currentPoint;
                        _lastDirection = currentDirection;
                    }
                }
                break;


            case SKTouchAction.Released:
            case SKTouchAction.Cancelled:
                _isTracking = false;
                break;
        }

        e.Handled = true;
    }

    private Direction GetDirection(Point startPoint, Point endPoint)
    {
        var xDiff = endPoint.X - startPoint.X;
        var yDiff = endPoint.Y - startPoint.Y;

        if (Math.Abs(xDiff) > Math.Abs(yDiff))
        {
            return xDiff > 0 ? Direction.Right : Direction.Left;
        }
        else
        {
            return yDiff > 0 ? Direction.Down : Direction.Up;
        }
    }

    enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down
    }
}