package com.unity3d.player;

final class m {
    private static boolean a = false;
    private boolean b = false;
    private boolean c = false;
    private boolean d = true;
    private boolean e = false;

    m() {
    }

    static void a() {
        a = true;
    }

    static void b() {
        a = false;
    }

    static boolean c() {
        return a;
    }

    final void a(boolean z) {
        this.b = z;
    }

    final void b(boolean z) {
        this.d = z;
    }

    final void c(boolean z) {
        this.e = z;
    }

    final void d(boolean z) {
        this.c = z;
    }

    final boolean d() {
        return this.d;
    }

    final boolean e() {
        return this.e;
    }

    final boolean e(boolean z) {
        if (a) {
            return ((!z && !this.b) || this.d || this.c) ? false : true;
        }
        return false;
    }

    final boolean f() {
        return this.c;
    }

    public final String toString() {
        return super.toString();
    }
}
