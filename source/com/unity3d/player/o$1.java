package com.unity3d.player;

final class o$1 implements Runnable {
    final String a;
    final int b;
    final int c;
    final int d;
    final boolean e;
    final long f;
    final long g;
    final o h;

    o$1(o oVar, String str, int i, int i2, int i3, boolean z, long j, long j2) {
        this.h = oVar;
        this.a = str;
        this.b = i;
        this.c = i2;
        this.d = i3;
        this.e = z;
        this.f = j;
        this.g = j2;
    }

    @Override
    public final void run() {
        if (o.a(this.h) != null) {
            f.Log(5, "Video already playing");
            o.a(this.h, 2);
            o.b(this.h).release();
        } else {
            o.a(this.h, new n(o.c(this.h), this.a, this.b, this.c, this.d, this.e, this.f, this.g, new o$1$1(this)));
            if (o.a(this.h) != null) {
                o.g(this.h).addView(o.a(this.h));
            }
        }
    }
}
