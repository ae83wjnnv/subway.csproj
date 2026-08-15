package com.unity3d.player;

import java.lang.reflect.Member;

class ReflectionHelper$a {
    public volatile Member a;
    private final Class b;
    private final String c;
    private final String d;
    private final int e;

    ReflectionHelper$a(Class cls, String str, String str2) {
        this.b = cls;
        this.c = str;
        this.d = str2;
        this.e = ((((cls.hashCode() + 527) * 31) + this.c.hashCode()) * 31) + this.d.hashCode();
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (obj instanceof ReflectionHelper$a) {
            ReflectionHelper$a reflectionHelper$a = (ReflectionHelper$a) obj;
            if (this.e == reflectionHelper$a.e && this.d.equals(reflectionHelper$a.d) && this.c.equals(reflectionHelper$a.c) && this.b.equals(reflectionHelper$a.b)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        return this.e;
    }
}
