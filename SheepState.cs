using System;
using System.Collections.Generic;

namespace WolfvsSheep
{
    class SheepState : IEquatable<SheepState>
    {
        public byte S0, S1, S2, S3, S4;

        public SheepState( byte s0, byte s1, byte s2, byte s3, byte s4)
        {
            this.S0 = s0;
            this.S1 = s1;
            this.S2 = s2;
            this.S3 = s3;
            this.S4 = s4;
        }
        
        public override bool Equals(object obj)
        {
            return Equals(obj as SheepState);
        }

        public bool Equals(SheepState other)
        {
            if (other == null)
                return false;

            return this.S0 == other.S0 && this.S1 == other.S1 && this.S2 == other.S2 && this.S3 == other.S3 && this.S4 == other.S4;
        }


        // Compute hash as bit field
        public override int GetHashCode()
        {
            return S0 | S1 << 6 | S2 << 12 | S3 << 18 | S4 << 24;
        }
    }
}
