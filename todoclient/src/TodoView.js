import React, { useCallback } from 'react'
import { Stack } from 'txstate-react/lib/components'
import { useEvent } from 'txstate-react/lib/hooks'
import styled from 'styled-components'

const Description = styled.span`
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
`

const Todo = props => {
  return (
    <TodoContainer horizontal verticalAlign='center' horizontalAlign='space-between'>
      <Description>{props.text}</Description>
      <DueDate>{new Date(props.due).toLocaleDateString()}</DueDate>
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
      {data.reverse().map(item => <Todo text={item.text} due={item.dueDate} key={item.id} />)}
    </Stack>
  )
}
