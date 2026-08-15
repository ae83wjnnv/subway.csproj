package com.unity3d.player;

final class UnityPlayer$10 extends UnityPlayer$f {
    final boolean a;
    final String b;
    final int c;
    final UnityPlayer d;

    UnityPlayer$10(UnityPlayer unityPlayer, boolean z, String str, int i) {
        super(unityPlayer, (byte) 0);
        this.d = unityPlayer;
        this.a = z;
        this.b = str;
        this.c = i;
    }

    @Override
    public final void a() {
        if (this.a) {
            UnityPlayer.access$2700(this.d);
        } else {
            String str = this.b;
            if (str != null) {
                UnityPlayer.access$2800(this.d, str);
            }
        }
        if (this.c == 1) {
            UnityPlayer.access$2900(this.d);
        }
    }
}
