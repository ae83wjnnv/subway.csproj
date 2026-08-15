package com.unity3d.player;

import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;

class UnityPlayer$a implements SensorEventListener {
    final UnityPlayer a;

    UnityPlayer$a(UnityPlayer unityPlayer) {
        this.a = unityPlayer;
    }

    @Override
    public final void onAccuracyChanged(Sensor sensor, int i) {
    }

    @Override
    public final void onSensorChanged(SensorEvent sensorEvent) {
    }
}
