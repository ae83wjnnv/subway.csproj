package bitter.jnibridge;

import java.lang.invoke.MethodHandles$Lookup;
import java.lang.reflect.Constructor;
import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;

class JNIBridge$a implements InvocationHandler {
    private Object a = new Object[0];
    private long b;
    private Constructor c;

    public JNIBridge$a(long j) {
        this.b = j;
        try {
            Constructor declaredConstructor = MethodHandles$Lookup.class.getDeclaredConstructor(Class.class, Integer.TYPE);
            this.c = declaredConstructor;
            declaredConstructor.setAccessible(true);
        } catch (NoClassDefFoundError unused) {
            this.c = null;
        } catch (NoSuchMethodException unused2) {
            this.c = null;
        }
    }

    private Object a(Object obj, Method method, Object[] objArr) {
        if (objArr == null) {
            objArr = new Object[0];
        }
        Class<?> declaringClass = method.getDeclaringClass();
        return ((MethodHandles$Lookup) this.c.newInstance(declaringClass, 2)).in(declaringClass).unreflectSpecial(method, declaringClass).bindTo(obj).invokeWithArguments(objArr);
    }

    public final void a() {
        synchronized (this.a) {
            this.b = 0L;
        }
    }

    public final void finalize() {
        synchronized (this.a) {
            if (this.b == 0) {
                return;
            }
            JNIBridge.delete(this.b);
        }
    }

    @Override
    public final Object invoke(Object obj, Method method, Object[] objArr) {
        synchronized (this.a) {
            if (this.b == 0) {
                return null;
            }
            try {
                return JNIBridge.invoke(this.b, method.getDeclaringClass(), method, objArr);
            } catch (NoSuchMethodError e) {
                if (this.c == null) {
                    System.err.println("JNIBridge error: Java interface default methods are only supported since Android Oreo");
                    throw e;
                }
                if ((method.getModifiers() & 1024) == 0) {
                    return a(obj, method, objArr);
                }
                throw e;
            }
        }
    }
}
