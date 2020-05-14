
content of:
    \ml-agents\config\mlkicker_config.yaml

'''
MLKicker:
  trainer: ppo
  batch_size: 10
  beta: 5.0e-3
  buffer_size: 100
  epsilon: 0.2
  hidden_units: 128
  lambd: 0.95
  learning_rate: 3.0e-4
  learning_rate_schedule: linear
  max_steps: 5.0e4
  normalize: false
  num_epoch: 3
  num_layers: 2
  time_horizon: 64
  summary_freq: 10000
  use_recurrent: false
  reward_signals:
    extrinsic:
      strength: 1.0
      gamma: 0.99
  sequence_length: 64
  memory_size: 128
  '''