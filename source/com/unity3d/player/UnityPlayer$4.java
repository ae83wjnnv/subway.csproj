package com.unity3d.player;

final class UnityPlayer$4 implements Runnable {
    final UnityPlayer a;
    final String b;
    final int c;
    final boolean d;
    final boolean e;
    final boolean f;
    final boolean g;
    final String h;
    final int i;
    final boolean j;
    final boolean k;
    final UnityPlayer l;

    UnityPlayer$4(UnityPlayer unityPlayer, UnityPlayer unityPlayer2, String str, int i, boolean z, boolean z2, boolean z3, boolean z4, String str2, int i2, boolean z5, boolean z6) {
        this.l = unityPlayer;
        this.a = unityPlayer2;
        this.b = str;
        this.c = i;
        this.d = z;
        this.e = z2;
        this.f = z3;
        this.g = z4;
        this.h = str2;
        this.i = i2;
        this.j = z5;
        this.k = z6;
    }

    @Override
    public final void run() {
        this.l.mSoftInputDialog = new i(UnityPlayer.access$2300(this.l), this.a, this.b, this.c, this.d, this.e, this.f, this.h, this.i, this.j, this.k);
        this.l.mSoftInputDialog.setOnCancelListener(new UnityPlayer$4$1(this));
        this.l.mSoftInputDialog.show();
        UnityPlayer.access$2500(this.l);
    }
}
