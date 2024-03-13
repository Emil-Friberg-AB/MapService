namespace Domain.Models
{
    public class Polyline
    {
        private string _encodedPolyline;
        public string EncodedPolyline
        {
            get => _encodedPolyline;
            set
            {
                _encodedPolyline = value;
                DecodedPolyLine = DecodePolyline(value);
            }
        }
        public List<LatLng> DecodedPolyLine { get; set; }

        public static List<LatLng> DecodePolyline(string encodedPoints)
        {
            if (string.IsNullOrEmpty(encodedPoints))
                return new List<LatLng>();

            var poly = new List<LatLng>();
            var index = 0;
            var len = encodedPoints.Length;
            var lat = 0;
            var lng = 0;

            while (index < len)
            {
                int b, shift = 0, result = 0;
                do
                {
                    b = encodedPoints[index++] - 63;
                    result |= (b & 0x1f) << shift;
                    shift += 5;
                } while (b >= 0x20);

                int dlat = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
                lat += dlat;

                shift = 0;
                result = 0;
                do
                {
                    b = encodedPoints[index++] - 63;
                    result |= (b & 0x1f) << shift;
                    shift += 5;
                } while (b >= 0x20);

                int dlng = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
                lng += dlng;

                LatLng p = new LatLng();
                p.Latitude = lat / 1E5;
                p.Longitude = lng / 1E5;
                poly.Add(p);
            }

            return poly;
        }
    }
}
