package com.unity3d.player;

import java.lang.invoke.MethodHandles$Lookup;
import java.lang.reflect.Constructor;
import java.lang.reflect.Method;

class ReflectionHelper$1 implements ReflectionHelper$c {
    final long a;
    final UnityPlayer b;
    final Class[] c;
    private Runnable d;
    private UnityPlayer e;
    private long f = ReflectionHelper.a();
    private long g;
    private boolean h;

    ReflectionHelper$1(long j, UnityPlayer unityPlayer, Class[] clsArr) {
        this.a = j;
        this.b = unityPlayer;
        this.c = clsArr;
        this.d = new ReflectionHelper$b(ReflectionHelper.a(), this.a);
        this.e = this.b;
    }

    private Object a(Object obj, Method method, Object[] objArr) throws NoSuchMethodException {
        if (objArr == null) {
            try {
                objArr = new Object[0];
            } catch (NoClassDefFoundError unused) {
                f.Log(6, String.format("Java interface default methods are only supported since Android Oreo", new Object[0]));
                ReflectionHelper.a(this.g);
                return null;
            }
        }
        Class<?> declaringClass = method.getDeclaringClass();
        Constructor declaredConstructor = MethodHandles$Lookup.class.getDeclaredConstructor(Class.class, Integer.TYPE);
        declaredConstructor.setAccessible(true);
        return ((MethodHandles$Lookup) declaredConstructor.newInstance(declaringClass, 2)).in(declaringClass).unreflectSpecial(method, declaringClass).bindTo(obj).invokeWithArguments(objArr);
    }

    @Override
    public final void a(long j, boolean z) {
        this.g = j;
        this.h = z;
    }

    protected final void finalize() throws Throwable {
        this.e.queueGLThreadEvent(this.d);
        super.finalize();
    }

    @Override
    public final Object invoke(Object obj, Method method, Object[] objArr) {
        long j;
        if (!ReflectionHelper.beginProxyCall(this.f)) {
            f.Log(6, "Scripting proxy object was destroyed, because Unity player was unloaded.");
            return null;
        }
        try {
            this.g = 0L;
            this.h = false;
            Object objA = ReflectionHelper.a(this.a, method.getName(), objArr);
            if (!this.h) {
                if (this.g != 0) {
                    j = this.g;
                }
                return objA;
            }
            if ((method.getModifiers() & 1024) == 0) {
                return a(obj, method, objArr);
            }
            j = this.g;
            ReflectionHelper.a(j);
            return objA;
        } finally {
            ReflectionHelper.endProxyCall();
        }
    }
}
