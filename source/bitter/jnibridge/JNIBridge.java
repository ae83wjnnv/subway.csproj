package bitter.jnibridge;

import java.lang.reflect.Method;
import java.lang.reflect.Proxy;

public class JNIBridge {
    static native void delete(long j);

    static void disableInterfaceProxy(Object obj) {
        if (obj != null) {
            ((JNIBridge$a) Proxy.getInvocationHandler(obj)).a();
        }
    }

    static native Object invoke(long j, Class cls, Method method, Object[] objArr);

    static Object newInterfaceProxy(long j, Class[] clsArr) {
        return Proxy.newProxyInstance(JNIBridge.class.getClassLoader(), clsArr, new JNIBridge$a(j));
    }
}
