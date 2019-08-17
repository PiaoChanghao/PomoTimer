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
using Unity.UIWidgets.async;

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

        private string mTimeText = "25:00";
        private string mButtonText = "START";

        private Timer mTimer = null;
        private Stopwatch mStopwatch = null;

        public override void initState()
        {
            base.initState();

            mStopwatch = new Stopwatch();

            mTimer = Window.instance.periodic(DELAY, () =>
            {
                var minutes = (int)mStopwatch.Elapsed.TotalMinutes;

                if (minutes == 25)
                {
                    Debug.Log("到达25分钟");
                    return;
                }



                Debug.LogFormat("   DELAYED:{0}", mStopwatch.Elapsed.TotalSeconds);

                mTimeText = $"{24 - mStopwatch.Elapsed.Minutes}:{59 - mStopwatch.Elapsed.Seconds}";

                if (mStopwatch.IsRunning)
                {
                    this.setState(() =>
                    {
                        mButtonText = "RUNNING";
                    });
                }
                else if ((int)mStopwatch.Elapsed.TotalSeconds == 0)
                {
                    this.setState(() => { mTimeText = "25:00"; });

                }
                else
                {
                    this.setState(() => { mButtonText = "PAUSED"; });
                }
            });
        }

        public override void dispose()
        {
            base.dispose();

            mStopwatch.Stop();
            mTimer.cancel();
        }

        public override Widget build(BuildContext context)
        {
            return
                new Unity.UIWidgets.widgets.Stack(
                    children: new List<Widget>()
                    {
                        new Align(
                    alignment: Alignment.center,
                    child: new Text(mTimeText, style: new TextStyle(
                    color: Colors.black,
                    fontSize: 54,
                    fontWeight: FontWeight.bold
                )
                    )),
                        new Align(
                            alignment:Alignment.bottomCenter,
                            child:new Container(
                                margin:EdgeInsets.only(bottom:32),
                                child:new GestureDetector(
                                    child:new RoundedButton(mButtonText),
                                    onTap: () =>
                                    {
                                        if (mStopwatch.IsRunning)
                                        {
                                            mStopwatch.Stop();
                                        }
                                        else
                                        {
                                            mStopwatch.Start();
                                        }
                                    }
                                  )
                            )
                        )
                    }
                );
        }
    }

}


