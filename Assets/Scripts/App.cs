using System.Collections;
using System.Collections.Generic;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;
using System;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace PomoTimerApp
{
    public class App : UIWidgetsPanel
    {
        protected override Widget createWidget()
        {
            return new TimerPage();   
        }   
    }

    public class TimerPage : StatefulWidget
    {    
        public override State createState()
        {
            return new TimerPageState();
        }
    }

    class TimerPageState : State<TimerPage>
    {

        public readonly static TimeSpan DELAY = TimeSpan.FromMilliseconds(100);

        string mTimeText = "25:00";

        public override void initState()
        {   
            base.initState();

            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var timer = Window.instance.periodic(DELAY, () =>
            {
                Debug.LogFormat("   DELAYED:{0}", stopwatch.Elapsed.TotalSeconds);

                this.setState(() =>
                {
                    mTimeText = $"{24 - stopwatch.Elapsed.Minutes}:{59  -stopwatch.Elapsed.Seconds}";
                });
            });
        }

        public override Widget build(BuildContext context)
        {
            return new Text(mTimeText, style: new TextStyle(
                color: Colors.white,
                fontSize: 50
                ));
        }
    }

}


