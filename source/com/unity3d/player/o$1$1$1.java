package com.unity3d.player;

final class o$1$1$1 implements Runnable {
    final o$1$1 a;

    o$1$1$1(o$1$1 o_1_1) {
        this.a = o_1_1;
    }

    @Override
    public final void run() {
        o.f(this.a.a.h);
        o.g(this.a.a.h).resume();
    }
}
