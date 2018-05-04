namespace BusinessDomain {
    
    public sealed class SpatialExtend {
    
        public Position TopLeft { get; set; }
        
        public Position BottomRight { get; set; }

        public bool IsWithin(Position position) {
            return true;
        }
    }
}
