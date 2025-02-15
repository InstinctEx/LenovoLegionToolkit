﻿using System.Runtime.InteropServices;

namespace LenovoLegionToolkit.Lib.Features
{
    public class FlipToStartFeature : AbstractUEFIFeature<FlipToStartState>
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct FlipToBootStruct
        {
            [MarshalAs(UnmanagedType.U1)]
            public byte FlipToBootEn;
            [MarshalAs(UnmanagedType.U1)]
            public byte Reserved1;
            [MarshalAs(UnmanagedType.U1)]
            public byte Reserved2;
            [MarshalAs(UnmanagedType.U1)]
            public byte Reserved3;
        }

        public FlipToStartFeature() : base("{D743491E-F484-4952-A87D-8D5DD189B70C}", "FBSWIF", 7) { }

        public override FlipToStartState GetState()
        {
            var result = ReadFromUefi(new FlipToBootStruct
            {
                FlipToBootEn = 0,
                Reserved1 = 0,
                Reserved2 = 0,
                Reserved3 = 0
            });

            return result.FlipToBootEn == 0 ? FlipToStartState.Off : FlipToStartState.On;
        }

        public override void SetState(FlipToStartState state)
        {
            var structure = new FlipToBootStruct
            {
                FlipToBootEn = state == FlipToStartState.On ? (byte)1 : (byte)0,
                Reserved1 = 0,
                Reserved2 = 0,
                Reserved3 = 0
            };
            WriteToUefi(structure);
        }
    }
}
