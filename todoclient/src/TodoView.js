import React, { useCallback, useState } from 'react'
import { Stack } from 'txstate-react/lib/components'
import { useEvent } from 'txstate-react/lib/hooks'
import styled from 'styled-components'
import { todoAPI } from './TodoAPI'
import dayjs from 'dayjs'

const Description = styled.label`
  font-size: 1.2rem;
`

const DueDate = styled.span`
  font-size: 1rem;
`

const TodoContainer = styled(Stack)`
  width: 100%;
  border-radius: 3px;
  border: solid 1px #30303030;
  padding: 8px 12px;

  > * {
    text-decoration: ${({ done }) => done ? 'line-through' : 'none'};
  }
`

const Todo = props => {
  const { done, id, text, due } = props
  const [checked, setChecked] = useState(Boolean(done))
  const broadcastUpdate = useEvent('created-todo')

  const handleOnChange = useCallback(async (e) => {
    setChecked(e.target.checked)
    await todoAPI.post('/update', {
      id: id,
      text: text,
      due: dayjs(due),
      done: e.target.checked
    })
    broadcastUpdate()
  }, [setChecked, broadcastUpdate, id, text, due])

  return (
    <TodoContainer done={checked} horizontal verticalAlign='center' horizontalAlign='space-between' spacing={12}>
      <Stack.Item>
        <input id={id} type='checkbox' style={{ marginRight: 12 }} checked={checked} onChange={handleOnChange} />
        <Description htmlFor={id}>{text}</Description>
      </Stack.Item>
      <DueDate>{dayjs(due).format('MM/DD/YYYY')}</DueDate>
    </TodoContainer>
  )
}

export const TodoView = props => {
  const { data, reload } = props

  const handleNewTodos = useCallback(() => {
    reload()
  }, [reload])

  useEvent('created-todo', handleNewTodos)

  return (
    <Stack spacing={16}>
      {data.map(item => <Todo id={item.id} text={item.text} due={item.due} key={item.id} done={item.done} />)}
    </Stack>
  )
}
