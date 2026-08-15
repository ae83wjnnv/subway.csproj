package com.unity3d.player;

import android.net.ConnectivityManager$NetworkCallback;
import android.net.Network;
import android.net.NetworkCapabilities;

final class NetworkConnectivity$1 extends ConnectivityManager$NetworkCallback {
    final NetworkConnectivity a;

    NetworkConnectivity$1(NetworkConnectivity networkConnectivity) {
        this.a = networkConnectivity;
    }

    @Override
    public final void onAvailable(Network network) {
        super.onAvailable(network);
    }

    @Override
    public final void onCapabilitiesChanged(Network network, NetworkCapabilities networkCapabilities) {
        NetworkConnectivity networkConnectivity;
        int i;
        super.onCapabilitiesChanged(network, networkCapabilities);
        if (networkCapabilities.hasTransport(0)) {
            networkConnectivity = this.a;
            i = 1;
        } else {
            networkConnectivity = this.a;
            i = 2;
        }
        NetworkConnectivity.a(networkConnectivity, i);
    }

    @Override
    public final void onLost(Network network) {
        super.onLost(network);
        NetworkConnectivity.a(this.a, 0);
    }

    @Override
    public final void onUnavailable() {
        super.onUnavailable();
        NetworkConnectivity.a(this.a, 0);
    }
}
