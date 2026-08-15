package com.unity3d.player;

final class o$1$1 implements n$a {
    final o$1 a;

    o$1$1(o$1 o_1) {
        this.a = o_1;
    }

    @Override
    public final void a(int i) {
        o.d(this.a.h).lock();
        o.a(this.a.h, i);
        if (i == 3 && o.e(this.a.h)) {
            this.a.h.runOnUiThread(new o$1$1$1(this));
        }
        if (i != 0) {
            o.b(this.a.h).release();
        }
        o.d(this.a.h).unlock();
    }
}
