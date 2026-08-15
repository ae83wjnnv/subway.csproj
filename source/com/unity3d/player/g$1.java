package com.unity3d.player;

final class g$1 implements Runnable {
    final String[] a;
    final g b;

    g$1(g gVar, String[] strArr) {
        this.b = gVar;
        this.a = strArr;
    }

    @Override
    public final void run() {
        g.a(this.b, this.a);
    }
}
