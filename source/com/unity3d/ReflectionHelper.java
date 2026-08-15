package com.unity3d.player;

import java.lang.reflect.Array;
import java.lang.reflect.Constructor;
import java.lang.reflect.Field;
import java.lang.reflect.Member;
import java.lang.reflect.Method;
import java.lang.reflect.Modifier;
import java.lang.reflect.Proxy;
import java.util.ArrayList;
import java.util.Iterator;

final class ReflectionHelper {
    protected static boolean LOG = false;
    protected static final boolean LOGV = false;
    private static ReflectionHelper$a[] a = new ReflectionHelper$a[4096];
    private static long b = 0;
    private static long c = 0;
    private static boolean d = false;

    ReflectionHelper() {
    }

    private static float a(Class cls, Class cls2) {
        if (cls.equals(cls2)) {
            return 1.0f;
        }
        if (cls.isPrimitive() || cls2.isPrimitive()) {
            return 0.0f;
        }
        try {
            if (cls.asSubclass(cls2) != null) {
                return 0.5f;
            }
        } catch (ClassCastException unused) {
        }
        try {
            return cls2.asSubclass(cls) != null ? 0.1f : 0.0f;
        } catch (ClassCastException unused2) {
            return 0.0f;
        }
    }

    private static float a(Class cls, Class[] clsArr, Class[] clsArr2) {
        if (clsArr2.length == 0) {
            return 0.1f;
        }
        int i = 0;
        if ((clsArr == null ? 0 : clsArr.length) + 1 != clsArr2.length) {
            return 0.0f;
        }
        float f = 1.0f;
        if (clsArr != null) {
            int length = clsArr.length;
            int i2 = 0;
            float fA = 1.0f;
            while (i < length) {
                fA *= a(clsArr[i], clsArr2[i2]);
                i++;
                i2++;
            }
            f = fA;
        }
        return f * a(cls, clsArr2[clsArr2.length - 1]);
    }

    static long a() {
        return b;
    }

    private static Class a(String str, int[] iArr) {
        while (iArr[0] < str.length()) {
            int i = iArr[0];
            iArr[0] = i + 1;
            char cCharAt = str.charAt(i);
            if (cCharAt != '(' && cCharAt != ')') {
                if (cCharAt == 'L') {
                    int iIndexOf = str.indexOf(59, iArr[0]);
                    if (iIndexOf == -1) {
                        return null;
                    }
                    String strSubstring = str.substring(iArr[0], iIndexOf);
                    iArr[0] = iIndexOf + 1;
                    try {
                        return Class.forName(strSubstring.replace('/', '.'));
                    } catch (ClassNotFoundException unused) {
                        return null;
                    }
                }
                if (cCharAt == 'Z') {
                    return Boolean.TYPE;
                }
                if (cCharAt == 'I') {
                    return Integer.TYPE;
                }
                if (cCharAt == 'F') {
                    return Float.TYPE;
                }
                if (cCharAt == 'V') {
                    return Void.TYPE;
                }
                if (cCharAt == 'B') {
                    return Byte.TYPE;
                }
                if (cCharAt == 'C') {
                    return Character.TYPE;
                }
                if (cCharAt == 'S') {
                    return Short.TYPE;
                }
                if (cCharAt == 'J') {
                    return Long.TYPE;
                }
                if (cCharAt == 'D') {
                    return Double.TYPE;
                }
                if (cCharAt == '[') {
                    return Array.newInstance((Class<?>) a(str, iArr), 0).getClass();
                }
                f.Log(5, "! parseType; " + cCharAt + " is not known!");
                return null;
            }
        }
        return null;
    }

    static Object a(long j, String str, Object[] objArr) {
        return nativeProxyInvoke(j, str, objArr);
    }

    static void a(long j) {
        nativeProxyLogJNIInvokeException(j);
    }

    private static synchronized void a(ReflectionHelper$a reflectionHelper$a, Member member) {
        reflectionHelper$a.a = member;
        a[reflectionHelper$a.hashCode() & (a.length - 1)] = reflectionHelper$a;
    }

    private static synchronized boolean a(ReflectionHelper$a reflectionHelper$a) {
        ReflectionHelper$a reflectionHelper$a2 = a[reflectionHelper$a.hashCode() & (a.length - 1)];
        if (!reflectionHelper$a.equals(reflectionHelper$a2)) {
            return false;
        }
        reflectionHelper$a.a = reflectionHelper$a2.a;
        return true;
    }

    private static Class[] a(String str) {
        Class clsA;
        int i = 0;
        int[] iArr = {0};
        ArrayList arrayList = new ArrayList();
        while (iArr[0] < str.length() && (clsA = a(str, iArr)) != null) {
            arrayList.add(clsA);
        }
        Class[] clsArr = new Class[arrayList.size()];
        Iterator it = arrayList.iterator();
        while (it.hasNext()) {
            clsArr[i] = (Class) it.next();
            i++;
        }
        return clsArr;
    }

    static void b(long j) {
        nativeProxyFinalize(j);
    }

    protected static synchronized boolean beginProxyCall(long j) {
        boolean z;
        if (j == b) {
            c++;
            z = true;
        } else {
            z = false;
        }
        return z;
    }

    protected static synchronized void endProxyCall() {
        long j = c - 1;
        c = j;
        if (0 == j && d) {
            ReflectionHelper.class.notifyAll();
        }
    }

    protected static synchronized void endUnityLaunch() {
        try {
            b++;
            d = true;
            while (c > 0) {
                ReflectionHelper.class.wait();
            }
        } catch (InterruptedException unused) {
            f.Log(6, "Interrupted while waiting for all proxies to exit.");
        }
        d = false;
    }

    protected static Constructor getConstructorID(Class cls, String str) {
        Constructor<?> constructor;
        ReflectionHelper$a reflectionHelper$a = new ReflectionHelper$a(cls, "", str);
        if (a(reflectionHelper$a)) {
            constructor = (Constructor) reflectionHelper$a.a;
        } else {
            Class[] clsArrA = a(str);
            float f = 0.0f;
            Constructor<?> constructor2 = null;
            for (Constructor<?> constructor3 : cls.getConstructors()) {
                float fA = a(Void.TYPE, constructor3.getParameterTypes(), clsArrA);
                if (fA > f) {
                    constructor2 = constructor3;
                    if (fA == 1.0f) {
                        break;
                    }
                    f = fA;
                }
            }
            a(reflectionHelper$a, constructor2);
            constructor = constructor2;
        }
        if (constructor != null) {
            return constructor;
        }
        throw new NoSuchMethodError("<init>" + str + " in class " + cls.getName());
    }

    protected static Field getFieldID(Class cls, String str, String str2, boolean z) {
        Field field;
        Class superclass = cls;
        ReflectionHelper$a reflectionHelper$a = new ReflectionHelper$a(superclass, str, str2);
        if (a(reflectionHelper$a)) {
            field = (Field) reflectionHelper$a.a;
        } else {
            Class[] clsArrA = a(str2);
            float f = 0.0f;
            Field field2 = null;
            while (superclass != null) {
                for (Field field3 : superclass.getDeclaredFields()) {
                    if (z == Modifier.isStatic(field3.getModifiers()) && field3.getName().compareTo(str) == 0) {
                        float fA = a(field3.getType(), (Class[]) null, clsArrA);
                        if (fA > f) {
                            field2 = field3;
                            if (fA == 1.0f) {
                                f = fA;
                                break;
                            }
                            f = fA;
                        } else {
                            continue;
                        }
                    }
                }
                if (f == 1.0f || superclass.isPrimitive() || superclass.isInterface() || superclass.equals(Object.class) || superclass.equals(Void.TYPE)) {
                    break;
                }
                superclass = superclass.getSuperclass();
            }
            a(reflectionHelper$a, field2);
            field = field2;
        }
        if (field != null) {
            return field;
        }
        Object[] objArr = new Object[4];
        objArr[0] = z ? "static" : "non-static";
        objArr[1] = str;
        objArr[2] = str2;
        objArr[3] = superclass.getName();
        throw new NoSuchFieldError(String.format("no %s field with name='%s' signature='%s' in class L%s;", objArr));
    }

    protected static String getFieldSignature(Field field) {
        Class<?> type = field.getType();
        if (!type.isPrimitive()) {
            if (type.isArray()) {
                return type.getName().replace('.', '/');
            }
            return "L" + type.getName().replace('.', '/') + ";";
        }
        String name = type.getName();
        if ("boolean".equals(name)) {
            return "Z";
        }
        if ("byte".equals(name)) {
            return "B";
        }
        if ("char".equals(name)) {
            return "C";
        }
        if ("double".equals(name)) {
            return "D";
        }
        if ("float".equals(name)) {
            return "F";
        }
        if ("int".equals(name)) {
            return "I";
        }
        if ("long".equals(name)) {
            return "J";
        }
        return "short".equals(name) ? "S" : name;
    }

    protected static Method getMethodID(Class cls, String str, String str2, boolean z) {
        Method method;
        ReflectionHelper$a reflectionHelper$a = new ReflectionHelper$a(cls, str, str2);
        if (a(reflectionHelper$a)) {
            method = (Method) reflectionHelper$a.a;
        } else {
            Class[] clsArrA = a(str2);
            float f = 0.0f;
            Method method2 = null;
            while (cls != null) {
                for (Method method3 : cls.getDeclaredMethods()) {
                    if (z == Modifier.isStatic(method3.getModifiers()) && method3.getName().compareTo(str) == 0) {
                        float fA = a(method3.getReturnType(), method3.getParameterTypes(), clsArrA);
                        if (fA > f) {
                            method2 = method3;
                            if (fA == 1.0f) {
                                f = fA;
                                break;
                            }
                            f = fA;
                        } else {
                            continue;
                        }
                    }
                }
                if (f == 1.0f || cls.isPrimitive() || cls.isInterface() || cls.equals(Object.class) || cls.equals(Void.TYPE)) {
                    break;
                }
                cls = cls.getSuperclass();
            }
            a(reflectionHelper$a, method2);
            method = method2;
        }
        if (method != null) {
            return method;
        }
        Object[] objArr = new Object[4];
        objArr[0] = z ? "static" : "non-static";
        objArr[1] = str;
        objArr[2] = str2;
        objArr[3] = cls.getName();
        throw new NoSuchMethodError(String.format("no %s method with name='%s' signature='%s' in class L%s;", objArr));
    }

    private static native void nativeProxyFinalize(long j);

    private static native Object nativeProxyInvoke(long j, String str, Object[] objArr);

    private static native void nativeProxyLogJNIInvokeException(long j);

    protected static Object newProxyInstance(UnityPlayer unityPlayer, long j, Class cls) {
        return newProxyInstance(unityPlayer, j, new Class[]{cls});
    }

    protected static Object newProxyInstance(UnityPlayer unityPlayer, long j, Class[] clsArr) {
        return Proxy.newProxyInstance(ReflectionHelper.class.getClassLoader(), clsArr, new ReflectionHelper$1(j, unityPlayer, clsArr));
    }

    protected static void setNativeExceptionOnProxy(Object obj, long j, boolean z) {
        ((ReflectionHelper$c) Proxy.getInvocationHandler(obj)).a(j, z);
    }
}
