package com.unity3d.player;

import android.app.Activity;
import android.content.Context;
import android.net.ConnectivityManager;
import android.net.ConnectivityManager$NetworkCallback;
import android.net.NetworkInfo;

public class NetworkConnectivity extends Activity {
    private int d;
    private ConnectivityManager e;
    private final int a = 0;
    private final int b = 1;
    private final int c = 2;
    private final ConnectivityManager$NetworkCallback f = new NetworkConnectivity$1(this);

    public NetworkConnectivity(Context context) {
        this.d = 0;
        ConnectivityManager connectivityManager = (ConnectivityManager) context.getSystemService("connectivity");
        this.e = connectivityManager;
        connectivityManager.registerDefaultNetworkCallback(this.f);
        NetworkInfo activeNetworkInfo = this.e.getActiveNetworkInfo();
        if (activeNetworkInfo == null || !activeNetworkInfo.isConnected()) {
            return;
        }
        this.d = activeNetworkInfo.getType() != 0 ? 2 : 1;
    }

    static int a(NetworkConnectivity networkConnectivity, int i) {
        networkConnectivity.d = i;
        return i;
    }

    public final int a() {
        return this.d;
    }

    public final void b() {
        this.e.unregisterNetworkCallback(this.f);
    }
}
