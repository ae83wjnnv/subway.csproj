package com.unity3d.player;

final class UnityPlayer$11 extends UnityPlayer$f {
    final int a;
    final int b;
    final UnityPlayer c;

    UnityPlayer$11(UnityPlayer unityPlayer, int i, int i2) {
        super(unityPlayer, (byte) 0);
        this.c = unityPlayer;
        this.a = i;
        this.b = i2;
    }

    @Override
    public final void a() {
        UnityPlayer.access$3000(this.c, this.a, this.b);
    }
}
